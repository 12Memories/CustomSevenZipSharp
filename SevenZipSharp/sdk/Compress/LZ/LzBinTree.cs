// LzBinTree.cs

using System;
using SevenZipSharp.sdk.Common;

namespace SevenZipSharp.sdk.Compress.LZ
{
    internal class BinTree : InWindow, IMatchFinder
	{
		uint _cyclicBufferPos;
		uint _cyclicBufferSize = 0;
		uint _matchMaxLen;

		uint[] _son;
		uint[] _hash;

		uint _cutValue = 0xFF;
		uint _hashMask;
		uint _hashSizeSum = 0;

		bool HASH_ARRAY = true;

		const uint kHash2Size = 1 << 10;
		const uint kHash3Size = 1 << 16;
		const uint kBT2HashSize = 1 << 16;
		const uint kStartMaxLen = 1;
		const uint kHash3Offset = kHash2Size;
		const uint kEmptyHashValue = 0;
		const uint kMaxValForNormalize = ((uint)1 << 31) - 1;
	
		uint kNumHashDirectBytes = 0;
		uint kMinMatchCheck = 4;
		uint kFixHashSize = kHash2Size + kHash3Size;
		
		public void SetType(int numHashBytes)
		{
			HASH_ARRAY = (numHashBytes > 2);
			if (HASH_ARRAY)
			{
				kNumHashDirectBytes = 0;
				kMinMatchCheck = 4;
				kFixHashSize = kHash2Size + kHash3Size;
			}
			else
			{
				kNumHashDirectBytes = 2;
				kMinMatchCheck = 2 + 1;
				kFixHashSize = 0;
			}
		}

		public new void SetStream(System.IO.Stream stream) { base.SetStream(stream); }
		public new void ReleaseStream() { base.ReleaseStream(); }
		
		public new void Init()
		{
			base.Init();
			for (uint i = 0; i < _hashSizeSum; i++)
				_hash[i] = kEmptyHashValue;
			_cyclicBufferPos = 0;
			ReduceOffsets(-1);
		}

		public new void MovePos()
		{
			if (++_cyclicBufferPos >= _cyclicBufferSize)
				_cyclicBufferPos = 0;
			base.MovePos();
			if (_pos == kMaxValForNormalize)
				Normalize();
		}

		public new byte GetIndexByte(int index) { return base.GetIndexByte(index); }

		public new uint GetMatchLen(int index, uint distance, uint limit)
		{ return base.GetMatchLen(index, distance, limit); }

		public new uint GetNumAvailableBytes() { return base.GetNumAvailableBytes(); }

		public void Create(uint historySize, uint keepAddBufferBefore,
				uint matchMaxLen, uint keepAddBufferAfter)
		{
			if (historySize > kMaxValForNormalize - 256)
				throw new Exception();
			_cutValue = 16 + (matchMaxLen >> 1);
				
			var windowReservSize = (historySize + keepAddBufferBefore +
					matchMaxLen + keepAddBufferAfter) / 2 + 256;

			base.Create(historySize + keepAddBufferBefore, matchMaxLen + keepAddBufferAfter, windowReservSize);

			_matchMaxLen = matchMaxLen;

			var cyclicBufferSize = historySize + 1;
			if (_cyclicBufferSize != cyclicBufferSize)
				_son = new uint[(_cyclicBufferSize = cyclicBufferSize) * 2];

			var hs = kBT2HashSize;

			if (HASH_ARRAY)
			{
				hs = historySize - 1;
				hs |= (hs >> 1);
				hs |= (hs >> 2);
				hs |= (hs >> 4);
				hs |= (hs >> 8);
				hs >>= 1;
				hs |= 0xFFFF;
				if (hs > (1 << 24))
					hs >>= 1;
				_hashMask = hs;
				hs++;
				hs += kFixHashSize;
			}
			if (hs != _hashSizeSum)
				_hash = new uint[_hashSizeSum = hs];
		}

		public uint GetMatches(uint[] distances)
		{
			uint lenLimit;
			if (_pos + _matchMaxLen <= _streamPos)
				lenLimit = _matchMaxLen;
			else
			{
				lenLimit = _streamPos - _pos;
				if (lenLimit < kMinMatchCheck)
				{
					MovePos();
					return 0;
				}
			}

			uint offset = 0;
			var matchMinPos = (_pos > _cyclicBufferSize) ? (_pos - _cyclicBufferSize) : 0;
			var cur = _bufferOffset + _pos;
			var maxLen = kStartMaxLen; // to avoid items for len < hashSize;
			uint hashValue, hash2Value = 0, hash3Value = 0;

			if (HASH_ARRAY)
			{
				var temp = CRC.Table[_bufferBase[cur]] ^ _bufferBase[cur + 1];
				hash2Value = temp & (kHash2Size - 1);
				temp ^= ((uint)(_bufferBase[cur + 2]) << 8);
				hash3Value = temp & (kHash3Size - 1);
				hashValue = (temp ^ (CRC.Table[_bufferBase[cur + 3]] << 5)) & _hashMask;
			}
			else
				hashValue = _bufferBase[cur] ^ ((uint)(_bufferBase[cur + 1]) << 8);

			var curMatch = _hash[kFixHashSize + hashValue];
			if (HASH_ARRAY)
			{
				var curMatch2 = _hash[hash2Value];
				var curMatch3 = _hash[kHash3Offset + hash3Value];
				_hash[hash2Value] = _pos;
				_hash[kHash3Offset + hash3Value] = _pos;
				if (curMatch2 > matchMinPos)
					if (_bufferBase[_bufferOffset + curMatch2] == _bufferBase[cur])
					{
						distances[offset++] = maxLen = 2;
						distances[offset++] = _pos - curMatch2 - 1;
					}
				if (curMatch3 > matchMinPos)
					if (_bufferBase[_bufferOffset + curMatch3] == _bufferBase[cur])
					{
						if (curMatch3 == curMatch2)
							offset -= 2;
						distances[offset++] = maxLen = 3;
						distances[offset++] = _pos - curMatch3 - 1;
						curMatch2 = curMatch3;
					}
				if (offset != 0 && curMatch2 == curMatch)
				{
					offset -= 2;
					maxLen = kStartMaxLen;
				}
			}

			_hash[kFixHashSize + hashValue] = _pos;

			var ptr0 = (_cyclicBufferPos << 1) + 1;
			var ptr1 = (_cyclicBufferPos << 1);

			uint len0, len1;
			len0 = len1 = kNumHashDirectBytes;
			
			if (kNumHashDirectBytes != 0)
			{
				if (curMatch > matchMinPos)
				{
					if (_bufferBase[_bufferOffset + curMatch + kNumHashDirectBytes] !=
							_bufferBase[cur + kNumHashDirectBytes])
					{
						distances[offset++] = maxLen = kNumHashDirectBytes;
						distances[offset++] = _pos - curMatch - 1;
					}
				}
			}
			
			var count = _cutValue;
			
			while(true)
			{
				if(curMatch <= matchMinPos || count-- == 0)
				{
					_son[ptr0] = _son[ptr1] = kEmptyHashValue;
					break;
				}
				var delta = _pos - curMatch;
				var cyclicPos = ((delta <= _cyclicBufferPos) ?
							(_cyclicBufferPos - delta) :
							(_cyclicBufferPos - delta + _cyclicBufferSize)) << 1;

				var pby1 = _bufferOffset + curMatch;
				var len = Math.Min(len0, len1);
				if (_bufferBase[pby1 + len] == _bufferBase[cur + len])
				{
					while(++len != lenLimit)
						if (_bufferBase[pby1 + len] != _bufferBase[cur + len])
							break;
					if (maxLen < len)
					{
						distances[offset++] = maxLen = len;
						distances[offset++] = delta - 1;
						if (len == lenLimit)
						{
							_son[ptr1] = _son[cyclicPos];
							_son[ptr0] = _son[cyclicPos + 1];
							break;
						}
					}
				}
				if (_bufferBase[pby1 + len] < _bufferBase[cur + len])
				{
					_son[ptr1] = curMatch;
					ptr1 = cyclicPos + 1;
					curMatch = _son[ptr1];
					len1 = len;
				}
				else
				{
					_son[ptr0] = curMatch;
					ptr0 = cyclicPos;
					curMatch = _son[ptr0];
					len0 = len;
				}
			}
			MovePos();
			return offset;
		}

		public void Skip(uint num)
		{
			do
			{
				uint lenLimit;
				if (_pos + _matchMaxLen <= _streamPos)
					lenLimit = _matchMaxLen;
				else
				{
					lenLimit = _streamPos - _pos;
					if (lenLimit < kMinMatchCheck)
					{
						MovePos();
						continue;
					}
				}

				var matchMinPos = (_pos > _cyclicBufferSize) ? (_pos - _cyclicBufferSize) : 0;
				var cur = _bufferOffset + _pos;

				uint hashValue;

				if (HASH_ARRAY)
				{
					var temp = CRC.Table[_bufferBase[cur]] ^ _bufferBase[cur + 1];
					var hash2Value = temp & (kHash2Size - 1);
					_hash[hash2Value] = _pos;
					temp ^= ((uint)(_bufferBase[cur + 2]) << 8);
					var hash3Value = temp & (kHash3Size - 1);
					_hash[kHash3Offset + hash3Value] = _pos;
					hashValue = (temp ^ (CRC.Table[_bufferBase[cur + 3]] << 5)) & _hashMask;
				}
				else
					hashValue = _bufferBase[cur] ^ ((uint)(_bufferBase[cur + 1]) << 8);

				var curMatch = _hash[kFixHashSize + hashValue];
				_hash[kFixHashSize + hashValue] = _pos;

				var ptr0 = (_cyclicBufferPos << 1) + 1;
				var ptr1 = (_cyclicBufferPos << 1);

				uint len0, len1;
				len0 = len1 = kNumHashDirectBytes;

				var count = _cutValue;
				while (true)
				{
					if (curMatch <= matchMinPos || count-- == 0)
					{
						_son[ptr0] = _son[ptr1] = kEmptyHashValue;
						break;
					}

					var delta = _pos - curMatch;
					var cyclicPos = ((delta <= _cyclicBufferPos) ?
								(_cyclicBufferPos - delta) :
								(_cyclicBufferPos - delta + _cyclicBufferSize)) << 1;

					var pby1 = _bufferOffset + curMatch;
					var len = Math.Min(len0, len1);
					if (_bufferBase[pby1 + len] == _bufferBase[cur + len])
					{
						while (++len != lenLimit)
							if (_bufferBase[pby1 + len] != _bufferBase[cur + len])
								break;
						if (len == lenLimit)
						{
							_son[ptr1] = _son[cyclicPos];
							_son[ptr0] = _son[cyclicPos + 1];
							break;
						}
					}
					if (_bufferBase[pby1 + len] < _bufferBase[cur + len])
					{
						_son[ptr1] = curMatch;
						ptr1 = cyclicPos + 1;
						curMatch = _son[ptr1];
						len1 = len;
					}
					else
					{
						_son[ptr0] = curMatch;
						ptr0 = cyclicPos;
						curMatch = _son[ptr0];
						len0 = len;
					}
				}
				MovePos();
			}
			while (--num != 0);
		}

		void NormalizeLinks(uint[] items, uint numItems, uint subValue)
		{
			for (uint i = 0; i < numItems; i++)
			{
				var value = items[i];
				if (value <= subValue)
					value = kEmptyHashValue;
				else
					value -= subValue;
				items[i] = value;
			}
		}

		void Normalize()
		{
			var subValue = _pos - _cyclicBufferSize;
			NormalizeLinks(_son, _cyclicBufferSize * 2, subValue);
			NormalizeLinks(_hash, _hashSizeSum, subValue);
			ReduceOffsets((int)subValue);
		}

		public void SetCutValue(uint cutValue) { _cutValue = cutValue; }
	}
}
