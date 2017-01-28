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

using System.ComponentModel;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
    /// <summary>
    /// The Graphic Control Extension contains parameters used when processing 
    /// a graphic rendering block. The scope of this extension is the first 
    /// graphic rendering block to follow. The extension contains only one 
    /// data sub-block.
    /// This block is OPTIONAL; at most one Graphic Control Extension may 
    /// precede a graphic rendering block. This is the only limit to the number 
    /// of Graphic Control Extensions that may be contained in a Data Stream.
    /// </summary>
    public class GraphicControlExtension : GifComponent
    {
        /// <summary>
        /// The expected block size for a Graphic Control Extension
        /// </summary>
        public const int ExpectedBlockSize = 4;

        #region declarations
        private int _blockSize;
        private DisposalMethod _disposalMethod;
        private bool _expectsUserInput;
        private bool _hasTransparentColour;
        private int _delayTime;
        private int _transparentColourIndex;
        #endregion

        #region constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="blockSize">
        /// Sets the <see cref="BlockSize"/>.
        /// </param>
        /// <param name="disposalMethod">
        /// Sets the <see cref="DisposalMethod"/>.
        /// </param>
        /// <param name="expectsUserInput">
        /// Sets the <see cref="ExpectsUserInput"/> flag.
        /// </param>
        /// <param name="hasTransparentColour">
        /// Sets the <see cref="HasTransparentColour"/> flag.
        /// </param>
        /// <param name="delayTime">
        /// Sets the <see cref="DelayTime"/>.
        /// </param>
        /// <param name="transparentColourIndex">
        /// Sets the <see cref="TransparentColourIndex"/>.
        /// </param>
        public GraphicControlExtension(int blockSize,
                                        DisposalMethod disposalMethod,
                                        bool expectsUserInput,
                                        bool hasTransparentColour,
                                        int delayTime,
                                        int transparentColourIndex)
        {
            _blockSize = blockSize;
            _disposalMethod = disposalMethod;
            _expectsUserInput = expectsUserInput;
            _hasTransparentColour = hasTransparentColour;
            _delayTime = delayTime;
            _transparentColourIndex = transparentColourIndex;
        }
        #endregion

        #region constructor( Stream )
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputStream">
        /// The input stream to read.
        /// </param>
        public GraphicControlExtension(Stream inputStream)
            : this(inputStream, false) { }
        #endregion

        #region constructor( Stream, bool )
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputStream">
        /// The input stream to read.
        /// </param>
        /// <param name="xmlDebugging">Whether or not to create debug XML</param>
        public GraphicControlExtension(Stream inputStream, bool xmlDebugging)
            : base(xmlDebugging)
        {
            _blockSize = Read(inputStream); // block size

            PackedFields packed = new PackedFields(Read(inputStream));
            _disposalMethod = (DisposalMethod)packed.GetBits(3, 3);
            _expectsUserInput = packed.GetBit(6);
            _hasTransparentColour = packed.GetBit(7);

            if (_disposalMethod == 0)
            {
                _disposalMethod = DisposalMethod.DoNotDispose; // elect to keep old image if discretionary
            }

            _delayTime = ReadShort(inputStream); // delay in hundredths of a second
            _transparentColourIndex = Read(inputStream); // transparent color index
            int blockTerminator = Read(inputStream); // block terminator

            if (xmlDebugging)
            {
                WriteDebugXmlElement("BlockSize", _blockSize);

                WriteDebugXmlStartElement("PackedFields");
                WriteDebugXmlAttribute("ByteRead", ToHex(packed.Byte));
                WriteDebugXmlAttribute("DisposalMethod",
                                        _disposalMethod.ToString());
                WriteDebugXmlAttribute("ExpectsUserInput", _expectsUserInput);
                WriteDebugXmlAttribute("HasTransparentColour", _hasTransparentColour);
                WriteDebugXmlEndElement();

                WriteDebugXmlElement("DelayTime", _delayTime);
                WriteDebugXmlElement("TransparentColourIndex",
                                      _transparentColourIndex);
                WriteDebugXmlElement("BlockTerminator", blockTerminator);

                WriteDebugXmlFinish();
            }
        }
        #endregion

        #region logical properties

        #region BlockSize
        /// <summary>
        /// Number of bytes in the block, after the Block Size field and up to 
        /// but not including the Block Terminator.
        /// This field contains the fixed value 4.
        /// </summary>
        [Description("Number of bytes in the block, after the Block Size " +
                     "field and up to but not including the Block Terminator. " +
                     "This field contains the fixed value 4.")]
        public int BlockSize
        {
            get { return _blockSize; }
        }
        #endregion

        #region DisposalMethod
        /// <summary>
        /// Indicates the way in which the graphic is to be treated after being 
        /// displayed.
        /// </summary>
        [Description("Indicates the way in which the graphic is to be " +
                     "treated after being displayed.")]
        public DisposalMethod DisposalMethod
        {
            get { return _disposalMethod; }
        }
        #endregion

        #region ExpectsUserInput
        /// <summary>
        /// Indicates whether or not user input is expected before continuing. 
        /// If the flag is set, processing will continue when user input is 
        /// entered. 
        /// The nature of the User input is determined by the application 
        /// (Carriage Return, Mouse Button Click, etc.).
        /// 
        /// Values :    0 -   User input is not expected.
        ///             1 -   User input is expected.
        /// 
        /// When a Delay Time is used and the User Input Flag is set, 
        /// processing will continue when user input is received or when the
        /// delay time expires, whichever occurs first.
        /// </summary>
        [Description("Indicates whether or not user input is expected " +
                     "before continuing. If the flag is set, processing will " +
                     "continue when user input is entered. The nature of the " +
                     "User input is determined by the application (Carriage " +
                     "Return, Mouse Button Click, etc.). " +
                     "Values :    0 -   User input is not expected. " +
                     "1 -   User input is expected. " +
                     "When a Delay Time is used and the User Input Flag is " +
                     "set, processing will continue when user input is " +
                     "received or when the delay time expires, whichever " +
                     "occurs first.")]
        public bool ExpectsUserInput
        {
            get { return _expectsUserInput; }
        }
        #endregion

        #region HasTransparentColour
        /// <summary>
        /// Indicates whether a transparency index is given in the Transparent 
        /// Index field.
        /// </summary>
        [Description("Indicates whether a transparency index is given in " +
                     "the Transparent Index field.")]
        public bool HasTransparentColour
        {
            get { return _hasTransparentColour; }
        }
        #endregion

        #region DelayTime
        /// <summary>
        /// If not 0, this field specifies the number of hundredths (1/100) 
        /// of a second to wait before continuing with the processing of the 
        /// Data Stream. 
        /// The clock starts ticking immediately after the graphic is rendered. 
        /// This field may be used in conjunction with the User Input Flag field.
        /// </summary>
        [Description("If not 0, this field specifies the number of " +
                     "hundredths (1/100) of a second to wait before " +
                     "continuing with the processing of the Data Stream. " +
                     "The clock starts ticking immediately after the graphic " +
                     "is rendered. " +
                     "This field may be used in conjunction with the User " +
                     "Input Flag field.")]
        public int DelayTime
        {
            get { return _delayTime; }
        }
        #endregion

        #region TransparentColourIndex
        /// <summary>
        /// The Transparency Index is such that when encountered, the 
        /// corresponding pixel of the display device is not modified and 
        /// processing goes on to the next pixel. 
        /// The index is present if and only if the Transparency Flag is set 
        /// to 1.
        /// </summary>
        [Description("The Transparency Index is such that when encountered, " +
                     "the corresponding pixel of the display device is not " +
                     "modified and processing goes on to the next pixel. " +
                     "The index is present if and only if the Transparency " +
                     "Flag is set to 1.")]
        public int TransparentColourIndex
        {
            get { return _transparentColourIndex; }
        }
        #endregion

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

            PackedFields packed = new PackedFields();
            packed.SetBits(3, 3, (int)_disposalMethod);
            packed.SetBit(6, _expectsUserInput);
            packed.SetBit(7, _hasTransparentColour);
            WriteByte(packed.Byte, outputStream);

            WriteShort(_delayTime, outputStream);
            WriteByte(_transparentColourIndex, outputStream);
            WriteByte(0, outputStream); // block terminator
        }
        #endregion
    }
}
