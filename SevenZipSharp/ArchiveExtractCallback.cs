/*  This file is part of SevenZipSharp.

    SevenZipSharp is free software: you can redistribute it and/or modify
    it under the terms of the GNU Lesser General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    SevenZipSharp is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public License
    along with SevenZipSharp.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace SevenZipSharp
{
#if UNMANAGED
    /// <summary>
    /// Archive extraction callback to handle the process of unpacking files
    /// </summary>
    internal sealed class ArchiveExtractCallback : CallbackBase, IArchiveExtractCallback, ICryptoGetTextPassword,
        IDisposable
    {
        private List<uint> _actualIndexes;
        private Dictionary<uint, bool> _actualIndexesFastCheck;
        private Dictionary<uint, string> _altPaths;
        private IInArchive _archive;

        /// <summary>
        /// For Compressing event.
        /// </summary>
        private long _bytesCount;

        private long _bytesWritten;
        private long _bytesWrittenOld;
        private string _directory;

        /// <summary>
        /// Rate of the done work from [0, 1].
        /// </summary>
        private float _doneRate;

        private SevenZipExtractor _extractor;
        private FakeOutStreamWrapper _fakeStream;
        private uint? _fileIndex;
        private int _filesCount;
        private OutStreamWrapper _fileStream;
        private bool _directoryStructure;
        private int _currentIndex;

        //Modified
        private Dictionary<uint, OutStreamWrapper> _fileStreamDictionary;

        /// <summary> Usage: string legalFilename = InvalidFileNameCharsRegex.Replace(illegal, "_"); </summary>
        private static readonly Regex InvalidFileNameCharsRegex;

        /// <summary> Usage: string legalPath = InvalidPathCharsRegex.Replace(illegal, "_"); </summary>
        private static readonly Regex InvalidPathCharsRegex;

        const int MEMORY_PRESSURE = 64 * 1024 * 1024; //64mb seems to be the maximum value

        #region Constructors

        static ArchiveExtractCallback()
        {
            //  GetInvalidFileNameChars() is not complete.  Should also include: quote ("), less than (<), greater than (>), pipe (|), backspace (\b), null (\0), and tab (\t).
            var invalidFileNameChars = Path.GetInvalidFileNameChars() + "\"<>|\b\t\0";
            InvalidFileNameCharsRegex = new Regex(string.Format("[{0}]", Regex.Escape(invalidFileNameChars)),
                RegexOptions.Compiled | RegexOptions.CultureInvariant);

            //  GetInvalidPathChars() is not complete.  Should also include: quote ("), less than (<), greater than (>), pipe (|), backspace (\b), null (\0), and tab (\t).
            var invalidPathChars = Path.GetInvalidPathChars() + "\"<>|\b\t\0";
            InvalidPathCharsRegex = new Regex(string.Format("[{0}]", Regex.Escape(invalidPathChars)),
                RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }

        /// <summary>
        /// Initializes a new instance of the ArchiveExtractCallback class
        /// </summary>
        /// <param name="archive">IInArchive interface for the archive</param>
        /// <param name="directory">Directory where files are to be unpacked to</param>
        /// <param name="filesCount">The archive files count</param>'
        /// <param name="extractor">The owner of the callback</param>
        /// <param name="actualIndexes">The list of actual indexes (solid archives support)</param>
        /// <param name="directoryStructure">The value indicating whether to preserve directory structure of extracted files.</param>
        /// <param name="altPaths">Alt Paths.</param>
        public ArchiveExtractCallback(IInArchive archive, string directory, int filesCount, bool directoryStructure,
            List<uint> actualIndexes, Dictionary<uint, string> altPaths, SevenZipExtractor extractor)
        {
            Init(archive, directory, filesCount, directoryStructure, actualIndexes, altPaths, extractor);
        }

        /// <summary>   Initializes a new instance of the ArchiveExtractCallback class.</summary>
        /// <param name="archive">              IInArchive interface for the archive.</param>
        /// <param name="directory">            Directory where files are to be unpacked to.</param>
        /// <param name="filesCount">           The archive files count.</param>
        /// <param name="directoryStructure">   The value indicating whether to preserve directory
        ///                                     structure of extracted files.</param>
        /// <param name="actualIndexes">        The list of actual indexes (solid archives support)</param>
        /// <param name="altPaths">             Alt Paths.</param>
        /// <param name="password">             Password for the archive.</param>
        /// <param name="extractor">            The owner of the callback.</param>
        public ArchiveExtractCallback(IInArchive archive, string directory, int filesCount, bool directoryStructure,
            List<uint> actualIndexes, Dictionary<uint, string> altPaths, string password,
            SevenZipExtractor extractor) : base(password)
        {
            Init(archive, directory, filesCount, directoryStructure, actualIndexes, altPaths, extractor);
        }

        /// <summary>
        /// Initializes a new instance of the ArchiveExtractCallback class
        /// </summary>
        /// <param name="archive">IInArchive interface for the archive</param>
        /// <param name="stream">The stream where files are to be unpacked to</param>
        /// <param name="filesCount">The archive files count</param>
        /// <param name="fileIndex">The file index for the stream</param>
        /// <param name="extractor">The owner of the callback</param>
        public ArchiveExtractCallback(IInArchive archive, Stream stream, int filesCount, uint fileIndex,
            SevenZipExtractor extractor)
        {
            Init(archive, stream, filesCount, fileIndex, extractor);
        }

        /// <summary>
        /// Initializes a new instance of the ArchiveExtractCallback class
        /// </summary>
        /// <param name="archive">IInArchive interface for the archive</param>
        /// <param name="stream">The stream where files are to be unpacked to</param>
        /// <param name="filesCount">The archive files count</param>
        /// <param name="fileIndex">The file index for the stream</param>
        /// <param name="password">Password for the archive</param>
        /// <param name="extractor">The owner of the callback</param>
        public ArchiveExtractCallback(IInArchive archive, Stream stream, int filesCount, uint fileIndex,
            string password, SevenZipExtractor extractor) : base(password)
        {
            Init(archive, stream, filesCount, fileIndex, extractor);
        }

        /// <summary>
        /// Initializes a new instance of the ArchiveExtractCallback class
        /// </summary>
        /// <param name="archive">IInArchive interface for the archive</param>
        /// <param name="streamDictionary"></param>
        /// <param name="filesCount">The archive files count</param>
        /// <param name="extractor">The owner of the callback</param>
        public ArchiveExtractCallback(IInArchive archive, Dictionary<uint, Stream> streamDictionary, int filesCount,
            SevenZipExtractor extractor)
        {
            Init(archive, streamDictionary, filesCount, extractor);
        }

        /// <summary>
        /// Initializes a new instance of the ArchiveExtractCallback class
        /// </summary>
        /// <param name="archive">IInArchive interface for the archive</param>
        /// <param name="streamDictionary"></param>
        /// <param name="filesCount">The archive files count</param>
        /// <param name="password">Password for the archive</param>
        /// <param name="extractor">The owner of the callback</param>
        public ArchiveExtractCallback(IInArchive archive, Dictionary<uint, Stream> streamDictionary, int filesCount,
            string password, SevenZipExtractor extractor) : base(password)
        {
            Init(archive, streamDictionary, filesCount, extractor);
        }
        private void Init(IInArchive archive, string directory, int filesCount, bool directoryStructure,
            List<uint> actualIndexes, Dictionary<uint, string> altPaths, SevenZipExtractor extractor)
        {
            CommonInit(archive, filesCount, extractor);
            _directory = directory;
            _actualIndexes = actualIndexes;
            _actualIndexesFastCheck = new Dictionary<uint, bool>();
            if (actualIndexes != null)
            {
                foreach (var index in actualIndexes)
                    _actualIndexesFastCheck.Add(index, true);
            }

            _altPaths = altPaths;
            _directoryStructure = directoryStructure;
            if (!directory.EndsWith("" + Path.DirectorySeparatorChar, StringComparison.CurrentCulture))
            {
                _directory += Path.DirectorySeparatorChar;
            }
        }
        private void Init(IInArchive archive, Dictionary<uint, Stream> streamDictionary, int filesCount,
            SevenZipExtractor extractor)
        {
            CommonInit(archive, filesCount, extractor);
            _fileStreamDictionary = new Dictionary<uint, OutStreamWrapper>();
            foreach (var item in streamDictionary)
            {
                var fileStream = new OutStreamWrapper(item.Value, false);
                fileStream.BytesWritten += IntEventArgsHandler;
                _fileStreamDictionary.Add(item.Key, fileStream);

            }
        }
        private void Init(IInArchive archive, Stream stream, int filesCount, uint fileIndex,
            SevenZipExtractor extractor)
        {
            CommonInit(archive, filesCount, extractor);
            _fileStream?.Dispose();
            _fileStream = new OutStreamWrapper(stream, false);
            _fileStream.BytesWritten += IntEventArgsHandler;
            _fileIndex = fileIndex;
        }

        private void CommonInit(IInArchive archive, int filesCount, SevenZipExtractor extractor)
        {
            _archive = archive;
            _filesCount = filesCount;
            _fakeStream = new FakeOutStreamWrapper();
            _fakeStream.BytesWritten += IntEventArgsHandler;
            _extractor = extractor;
            GC.AddMemoryPressure(MEMORY_PRESSURE);
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when a new file is going to be unpacked
        /// </summary>
        /// <remarks>Occurs when 7-zip engine requests for an output stream for a new file to unpack in</remarks>
        public event EventHandler<FileInfoEventArgs> FileExtractionStarted;

        /// <summary>
        /// Occurs when a file has been successfully unpacked
        /// </summary>
        public event EventHandler<FileInfoEventArgs> FileExtractionFinished;

        /// <summary>
        /// Occurs when the archive is opened and 7-zip sends the size of unpacked data
        /// </summary>
        public event EventHandler<OpenEventArgs> Open;

        /// <summary>
        /// Occurs when the extraction is performed
        /// </summary>
        public event EventHandler<ProgressEventArgs> Extracting;

        /// <summary>
        /// Occurs during the extraction when a file already exists
        /// </summary>
        public event EventHandler<FileOverwriteEventArgs> FileExists;

        private void OnFileExists(FileOverwriteEventArgs e)
        {
            if (FileExists != null)
            {
                FileExists(this, e);
            }
        }

        private void OnOpen(OpenEventArgs e)
        {
            if (Open != null)
            {
                Open(this, e);
            }
        }

        private void OnFileExtractionStarted(FileInfoEventArgs e)
        {
            if (FileExtractionStarted != null)
            {
                FileExtractionStarted(this, e);
            }
        }

        private void OnFileExtractionFinished(FileInfoEventArgs e)
        {
            if (FileExtractionFinished != null)
            {
                FileExtractionFinished(this, e);
            }
        }

        private void OnExtracting(ProgressEventArgs e)
        {
            if (Extracting != null)
            {
                Extracting(this, e);
            }
        }

        private void IntEventArgsHandler(object sender, IntEventArgs e)
        {
            //prevent divide-by-zero
            if (_bytesCount == 0) _bytesCount = 1;
            var pold = (int) ((_bytesWrittenOld * 100) / _bytesCount);
            _bytesWritten += e.Value;
            var pnow = (int) ((_bytesWritten * 100) / _bytesCount);
            if (pnow <= pold) return;

            if (pnow > 100)
            {
                pold = pnow = 0;
            }

            _bytesWrittenOld = _bytesWritten;
            OnExtracting(new ProgressEventArgs((byte) pnow, (byte) (pnow - pold)));
        }

        #endregion

        /// <summary>
        /// Gives the size of the unpacked archive files
        /// </summary>
        /// <param name="total">Size of the unpacked archive files (in bytes)</param>
        public void SetTotal(ulong total)
        {
            _bytesCount = (long) total;
            OnOpen(new OpenEventArgs(total));
        }

        public void SetCompleted(ref ulong completeValue)
        {
        }

        /// <summary>
        /// Sets output stream for writing unpacked data
        /// </summary>
        /// <param name="index">Current file index</param>
        /// <param name="outStream">Output stream pointer</param>
        /// <param name="askExtractMode">Extraction mode</param>
        /// <returns>0 if OK</returns>
        public int GetStream(uint index, out ISequentialOutStream outStream, AskMode askExtractMode)
        {
            outStream = null;
            if (Canceled)
            {
                return -1;
            }

            _currentIndex = (int) index;
            if (askExtractMode == AskMode.Extract)
            {
                var fileName = _directory;
                if (!_fileIndex.HasValue && _fileStreamDictionary == null)
                {
                    #region Extraction to a file

                    if (_actualIndexes == null || _actualIndexesFastCheck.TryGetValue(index, out var dummyBool))
                    {
                        var data = new PropVariant();
                        _archive.GetProperty(index, ItemPropId.Path, ref data);
                        var entryName = NativeMethods.SafeCast(data, "");

                        #region Get entryName

                        if (string.IsNullOrEmpty(entryName))
                        {
                            if (_filesCount == 1)
                            {
                                //SAB: Sanitize before sending to Path, as archive can have invalid chars in it for Windows.
                                var sanitizedFileName = SanitizeStringAsValidPath(_extractor.FileName);
                                var archName = Path.GetFileName(sanitizedFileName);
                                archName = archName.Substring(0, archName.LastIndexOf('.'));
                                // Patch 17228: When the archive file count is 1, the code appends ".tar" to the extracted file name, that is incorrect. 
                                //if (!archName.EndsWith(".tar", StringComparison.OrdinalIgnoreCase))
                                //    archName += ".tar";
                                entryName = archName;
                            }
                            else
                            {
                                entryName = "[no name] " + index.ToString(CultureInfo.InvariantCulture);
                            }
                        }

                        #endregion

                        fileName = null;
                        if (_altPaths != null) _altPaths.TryGetValue(index, out fileName);
                        if (fileName == null)
                        {
                            //SAB: Sanitize before sending to Path, as archive can have invalid chars in it for Windows.
                            var sanitizedEntryName = SanitizeStringAsValidPath(entryName);
                            fileName = Path.Combine(_directory,
                                _directoryStructure ? sanitizedEntryName : Path.GetFileName(sanitizedEntryName));
                        }

                        _archive.GetProperty(index, ItemPropId.IsDirectory, ref data);
                        try
                        {
                            fileName = ValidateFileName(fileName);
                        }
                        catch (Exception e)
                        {
                            AddException(e);
                            goto FileExtractionStartedLabel;
                        }

                        if (!NativeMethods.SafeCast(data, false))
                        {
                            #region Branch

                            _archive.GetProperty(index, ItemPropId.LastWriteTime, ref data);
                            var time = NativeMethods.SafeCast(data, DateTime.MinValue);
                            if (File.Exists(fileName))
                            {
                                var fnea = new FileOverwriteEventArgs(fileName);
                                OnFileExists(fnea);
                                if (fnea.Cancel)
                                {
                                    Canceled = true;
                                    return -1;
                                }

                                if (string.IsNullOrEmpty(fnea.FileName))
                                {
                                    outStream = _fakeStream;
                                    goto FileExtractionStartedLabel;
                                }

                                fileName = fnea.FileName;
                            }

                            try
                            {
                                //Patch 16476: Jun 12, 2014 to prevent Antivirus from locking file when datetime set. (1 of 2 changes)
                                var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite,
                                    FileShare.ReadWrite);
                                if (_fileStream != null) _fileStream.Dispose();
                                _fileStream = new OutStreamWrapper(fileStream, fileName, time, true);
                                //_fileStream = new OutStreamWrapper(File.Create(fileName), fileName, time, true);
                            }
                            catch (Exception e)
                            {
                                if (e is FileNotFoundException)
                                {
                                    AddException(new IOException(
                                        "The file \"" + fileName +
                                        "\" was not extracted due to the File.Create fail."));
                                }
                                else
                                {
                                    AddException(e);
                                }

                                outStream = _fakeStream;
                                goto FileExtractionStartedLabel;
                            }

                            _fileStream.BytesWritten += IntEventArgsHandler;
                            outStream = _fileStream;

                            #endregion
                        }
                        else
                        {
                            #region Branch

                            if (!Directory.Exists(fileName))
                            {
                                try
                                {
                                    Directory.CreateDirectory(fileName);
                                }
                                catch (Exception e)
                                {
                                    AddException(e);
                                }

                                outStream = _fakeStream;
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        outStream = _fakeStream;
                    }

                    #endregion
                }
                else
                {
                    #region Extraction to a stream

                    if (_fileIndex.HasValue)
                    {
                        if (index == _fileIndex)
                        {
                            outStream = _fileStream;
                            _fileIndex = null;
                        }
                        else
                        {
                            outStream = _fakeStream;
                        }
                    }
                    else //Modified
                    {
                        var item = _fileStreamDictionary.ContainsKey(index);
                        if (item)
                            outStream = _fileStreamDictionary[index];
                        else
                            outStream = _fakeStream;
                    }

                    #endregion
                }

                FileExtractionStartedLabel:
                _doneRate += 1.0f / _filesCount;
                var iea = new FileInfoEventArgs(_extractor.ArchiveFileData[(int) index],
                    PercentDoneEventArgs.ProducePercentDone(_doneRate));
                OnFileExtractionStarted(iea);
                if (iea.Cancel)
                {
                    if (_fileStream != null)
                    {
                        _fileStream.Dispose();
                        _fileStream = null;
                    }

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (File.Exists(fileName))
                        {
                            try
                            {
                                File.Delete(fileName);
                            }
                            catch (Exception e)
                            {
                                AddException(e);
                            }
                        }
                    }

                    Canceled = true;
                    return -1;
                }
            }

            return 0;
        }

        public void PrepareOperation(AskMode askExtractMode)
        {
        }

        /// <summary>
        /// Called when the archive was extracted
        /// </summary>
        /// <param name="operationResult"></param>
        public void SetOperationResult(OperationResult operationResult)
        {
            //SAB: Dispose the file stream even if errors occur.
            if (_fileStream != null && !_fileIndex.HasValue)
            {
                try
                {
                    _fileStream.BytesWritten -= IntEventArgsHandler;
                    _fileStream.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }

                _fileStream = null;
            }

            if (operationResult != OperationResult.Ok && ReportErrors)
            {
                switch (operationResult)
                {
                    case OperationResult.CrcError:
                        AddException(new ExtractionFailedException("File is corrupted. Crc check has failed."));
                        break;
                    case OperationResult.DataError:
                        AddException(new ExtractionFailedException("File is corrupted. Data error has occurred."));
                        break;
                    case OperationResult.UnsupportedMethod:
                        AddException(new ExtractionFailedException("Unsupported method error has occurred."));
                        break;
                }
            }
            else
            {
                //SAB: Moved dispose to above if statement
                var iea = new FileInfoEventArgs(_extractor.ArchiveFileData[_currentIndex],
                    PercentDoneEventArgs.ProducePercentDone(_doneRate));
                OnFileExtractionFinished(iea);
                if (iea.Cancel)
                {
                    Canceled = true;
                }
            }
        }

        #region ICryptoGetTextPassword Members

        /// <summary>
        /// Sets password for the archive
        /// </summary>
        /// <param name="password">Password for the archive</param>
        /// <returns>Zero if everything is OK</returns>
        public int CryptoGetTextPassword(out string password)
        {
            password = Password;
            return 0;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            GC.RemoveMemoryPressure(MEMORY_PRESSURE);
            if (_fileStream != null)
            {
                try
                {
                    _fileStream.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }

                _fileStream = null;
            }

            if (_fakeStream != null)
            {
                try
                {
                    _fakeStream.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }

                _fakeStream = null;
            }
        }

        #endregion

        /// <summary>
        /// Validates the file name and ensures that the directory to the file name is valid and creates intermediate directories if necessary
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>The valid file name</returns>
        private static string ValidateFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new SevenZipArchiveException("some archive name is null or empty.");
            }

            var splittedFileName = new List<string>(fileName.Split(Path.DirectorySeparatorChar));
            foreach (var chr in Path.GetInvalidFileNameChars())
            {
                for (var i = 0; i < splittedFileName.Count; i++)
                {
                    if (chr == ':' && i == 0)
                    {
                        continue;
                    }

                    if (string.IsNullOrEmpty(splittedFileName[i]))
                    {
                        continue;
                    }

                    while (splittedFileName[i].IndexOf(chr) > -1)
                    {
                        splittedFileName[i] = splittedFileName[i].Replace(chr, '_');
                    }
                }
            }

            if (fileName.StartsWith(new string(Path.DirectorySeparatorChar, 2),
                StringComparison.CurrentCultureIgnoreCase))
            {
                splittedFileName.RemoveAt(0);
                splittedFileName.RemoveAt(0);
                splittedFileName[0] = new string(Path.DirectorySeparatorChar, 2) + splittedFileName[0];
            }

            if (splittedFileName.Count > 2)
            {
                var tfn = splittedFileName[0];
                for (var i = 1; i < splittedFileName.Count - 1; i++)
                {
                    tfn += Path.DirectorySeparatorChar + splittedFileName[i];
                }

                Directory.CreateDirectory(tfn);
            }

            return string.Join(new string(Path.DirectorySeparatorChar, 1), splittedFileName.ToArray());
        }

        /// <summary>   Given a proposed filename, returns a string that eliminates any disallowed characters.
        ///             If stripping characters results in an empty string, it returns '_'</summary>
        /// <param name="filename"> proposed filename.</param>
        /// <returns>   Sanitized filename.</returns>
        private static string SanitizeStringAsValidFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return filename;
            return InvalidFileNameCharsRegex.Replace(filename, "_");
        }

        /// <summary>   Given a proposed directory path, returns a string that eliminates any disallowed characters.
        ///             If stripping characters results in an empty string, it returns '_'
        /// </summary>
        /// <param name="pathname"> proposed pathname.</param>
        /// <returns>   Sanitized pathname.</returns>
        private static string SanitizeStringAsValidPath(string pathname)
        {
            return string.IsNullOrEmpty(pathname) ? pathname : InvalidPathCharsRegex.Replace(pathname, "_");
        }
    }
#endif
}
