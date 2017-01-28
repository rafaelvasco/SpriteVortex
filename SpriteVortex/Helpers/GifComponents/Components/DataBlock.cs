#region Copyright (C) Simon Bridewell
// 
// This file is part of the GifComponents library.
// GifComponents is free software; you can redistribute it and/or
// modify it under the terms of the Code Project Open License.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Code Project Open License for more details.
// 
// You can read the full text of the Code Project Open License at:
// http://www.codeproject.com/info/cpol10.aspx
//
// GifComponents is a derived work based on NGif written by gOODiDEA.NET
// and published at http://www.codeproject.com/KB/GDI-plus/NGif.aspx,
// with an enhancement by Phil Garcia published at
// http://www.thinkedge.com/blogengine/post/2008/02/20/Animated-GIF-Encoder-for-NET-Update.aspx
//
// Simon Bridewell makes no claim to be the original author of this library,
// only to have created a derived work.
#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Enums;

namespace SpriteVortex.Helpers.GifComponents.Components
{
    /// <summary>
    /// A data sub-block to form part of a Graphics Interchange Format data
    /// stream.
    /// See http://www.w3.org/Graphics/GIF/spec-gif89a.txt section 15.
    /// </summary>
    /// <remarks>
    /// Data Sub-blocks are units containing data. They do not have a label, 
    /// these blocks are processed in the context of control blocks, wherever 
    /// data blocks are specified in the format. The first byte of the Data 
    /// sub-block indicates the number of data bytes to follow. A data sub-block 
    /// may contain from 0 to 255 data bytes. The size of the block does not 
    /// account for the size byte itself, therefore, the empty sub-block is one 
    /// whose size field contains 0x00.
    /// </remarks>
    public class DataBlock : GifComponent
    {
        #region declarations
        private int _blockSize;
        private byte[] _data;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="blockSize">
        /// The number of bytes in the data block.
        /// </param>
        /// <param name="data">
        /// The bytes which make up the data in the data block.
        /// </param>
        public DataBlock(int blockSize, byte[] data)
        {
            SaveData(blockSize, data);
        }
        #endregion

        #region constructor( Stream )
        /// <summary>
        /// Reads the next variable length data block from the input stream.
        /// </summary>
        /// <param name="inputStream">
        /// The input stream to read.
        /// </param>
        public DataBlock(Stream inputStream)
            : this(inputStream, false)
        { }
        #endregion

        #region constructor( Stream, bool )
        /// <summary>
        /// Reads the next variable length data block from the input stream.
        /// </summary>
        /// <param name="inputStream">
        /// The input stream to read.
        /// </param>
        /// <param name="xmlDebugging">Whether or not to create debug XML</param>
        public DataBlock(Stream inputStream, bool xmlDebugging)
            : base(xmlDebugging)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }

            int blockSize = Read(inputStream);

            if (blockSize == -1)
            {
                // then we're at the end of the stream
                SaveData(0, new byte[0]);
                string message
                    = "The end of the input stream was reached whilst "
                    + "attempting to read a DataBlock.";
                SetStatus(ErrorState.EndOfInputStream, message);
                WriteDebugXmlFinish();
                return;
            }

            int bytesRead = 0;
            byte[] buffer;
            buffer = new byte[blockSize];
            if (blockSize > 0)
            {
                // keep reading until we've read the entire block
                int count = 0;
                while (bytesRead < blockSize)
                {
                    count = inputStream.Read(buffer, bytesRead, blockSize - bytesRead);
                    if (count == 0)
                    {
                        // then we've reached the end of the file
                        break;
                    }
                    bytesRead += count;
                }
            }

            SaveData(blockSize, buffer);

            WriteDebugXmlElement("BlockSize", blockSize);
            WriteDebugXmlByteValues("BytesRead", buffer);

            if (bytesRead < blockSize)
            {
                string message
                    = "Supplied block size: " + blockSize
                    + ". Actual block size: " + bytesRead;
                SetStatus(ErrorState.DataBlockTooShort, message);
            }

            WriteDebugXmlFinish();
        }
        #endregion

        #region private SaveData method
        private void SaveData(int blockSize, byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string message
                = "Supplied block size: " + blockSize
                + ". Actual block size: " + data.Length;

            if (blockSize > data.Length)
            {
                SetStatus(ErrorState.DataBlockTooShort, message);
            }

            if (blockSize < data.Length)
            {
                SetStatus(ErrorState.DataBlockTooLong, message);
            }

            _blockSize = blockSize;
            _data = data;
        }
        #endregion

        #region DeclaredBlockSize property
        /// <summary>
        /// Gets the block size held in the first byte of this data block.
        /// This should be the same as the actual length of the data block but
        /// may not be if the data block was instantiated from a corrupt stream
        /// - check the ErrorStatus property.
        /// </summary>
        public int DeclaredBlockSize
        {
            get { return _blockSize; }
        }
        #endregion

        #region ActualBlockSize property
        /// <summary>
        /// Gets the actual length of the data block.
        /// </summary>
        public int ActualBlockSize
        {
            get { return _data.Length; }
        }
        #endregion

        #region Data property
        /// <summary>
        /// Gets the byte array containing the data in this data sub-block.
        /// This does not include the first byte which holds the block size.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
                         "CA1819:PropertiesShouldNotReturnArrays")]
        public byte[] Data
        {
            get { return _data; }
        }
        #endregion

        #region indexer
        /// <summary>
        /// Gets a specific byte within the data block
        /// </summary>
        public byte this[int index]
        {
            get
            {
                if (index >= _data.Length)
                {
                    string message
                        = "Supplied index: " + index
                        + ". Array length: " + _data.Length;
                    throw new ArgumentOutOfRangeException("index", message);
                }
                return _data[index];
            }
        }
        #endregion

        #region public WriteToStream method
        /// <summary>
        /// Writes this component to the supplied output stream.
        /// </summary>
        /// <param name="outputStream">
        /// The output stream to write to.
        /// </param>
        public override void WriteToStream(Stream outputStream)
        {
            WriteByte(_blockSize, outputStream);
            foreach (byte b in _data)
            {
                WriteByte((int)b, outputStream);
            }
        }
        #endregion
    }
}
