#region Copyright (C) Simon Bridewell, Kevin Weiner
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
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using SpriteVortex.Helpers.GifComponents.Enums;
using SpriteVortex.Helpers.GifComponents.Pelettes;
using SpriteVortex.Helpers.GifComponents.Types;

namespace SpriteVortex.Helpers.GifComponents.Components
{
	/// <summary>
	/// A single image frame from a GIF file.
	/// Originally a nested class within the GifDecoder class by Kevin Weiner.
	/// Downloaded from 
	/// http://www.thinkedge.com/BlogEngine/file.axd?file=NGif_src2.zip
	///
	/// Amended by Simon Bridewell June 2009-February 2010:
	/// 1. Made member variables private.
	/// 2. Added various properties to expose all the elements of the GifFrame.
	/// 3. Added constructors for use in both encoding and decoding.
	/// 4. Derive from GifComponent.
	/// 5. Added constructor( Stream... )
	/// 6. Removed code to swap out transparent colour and replace with black
	///    (bug 2940635).
	/// 7. Corrected decoding of frames with transparent pixels (bug 2940669)
	/// </summary>
	[TypeConverter( typeof( ExpandableObjectConverter ) )]
	public class GifFrame : GifComponent
	{
		#region declarations
		private Image _image;
		private int _delay;
		private bool _expectsUserInput;
		private Point _position;
		private Palette _palette;
		private ColourTable _localColourTable;
		private GraphicControlExtension _extension;
		private ImageDescriptor _imageDescriptor;
		private Color _backgroundColour;
		private TableBasedImageData _indexedPixels;
		#endregion

		#region constructors
		
		#region constructor( Image )
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="theImage">
		/// The image held in this frame of the GIF file
		/// </param>
		public GifFrame( Image theImage )
		{
			_image = theImage;
			_delay = 10; // 10 1/100ths of a second, i.e. 1/10 of a second.
		}
		#endregion
		
		#region constructor( Stream, , , , ,  )
		/// <summary>
		/// Creates and returns a GifFrame by reading its data from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream containing the data which makes the GifStream, starting 
		/// with the image descriptor for this frame.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for the GIF stream.
		/// </param>
		/// <param name="gct">
		/// The global colour table for the GIF stream.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension, if any, which precedes this image in
		/// the input stream.
		/// </param>
		/// <param name="previousFrame">
		/// The frame which precedes this one in the GIF stream, if present.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which precedes the frame before this one in the GIF stream,
		/// if present.
		/// </param>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "2#gct")]
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "3#gce")]
		public GifFrame( Stream inputStream,
		                 LogicalScreenDescriptor lsd,
		                 ColourTable gct,
		                 GraphicControlExtension gce,
		                 GifFrame previousFrame,
		                 GifFrame previousFrameBut1 )
			: this( inputStream, lsd, gct, gce, previousFrame, previousFrameBut1, false )
		{
			
		}
		#endregion
		
		#region constructor( Stream, , , , , bool )
		/// <summary>
		/// Creates and returns a GifFrame by reading its data from the supplied
		/// input stream.
		/// </summary>
		/// <param name="inputStream">
		/// A stream containing the data which makes the GifStream, starting 
		/// with the image descriptor for this frame.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for the GIF stream.
		/// </param>
		/// <param name="gct">
		/// The global colour table for the GIF stream.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension, if any, which precedes this image in
		/// the input stream.
		/// </param>
		/// <param name="previousFrame">
		/// The frame which precedes this one in the GIF stream, if present.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which precedes the frame before this one in the GIF stream,
		/// if present.
		/// </param>
		/// <param name="xmlDebugging">Whether or not to create debug XML</param>
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "2#gct")]
		[SuppressMessage("Microsoft.Naming", 
		                 "CA1704:IdentifiersShouldBeSpelledCorrectly", 
		                 MessageId = "3#gce")]
		public GifFrame( Stream inputStream,
		                 LogicalScreenDescriptor lsd,
		                 ColourTable gct,
		                 GraphicControlExtension gce,
		                 GifFrame previousFrame,
		                 GifFrame previousFrameBut1, 
		                 bool xmlDebugging )
			: base( xmlDebugging )
		{
			#region guard against null arguments
			if( lsd == null )
			{
				throw new ArgumentNullException( "lsd" );
			}
			
			if( gce == null )
			{
				SetStatus( ErrorState.NoGraphicControlExtension, "" );
				// use a default GCE
				gce = new GraphicControlExtension( GraphicControlExtension.ExpectedBlockSize, 
				                                   DisposalMethod.NotSpecified, 
				                                   false, 
				                                   false, 
				                                   100, 
				                                   0 );
			}
			#endregion
			
			int transparentColourIndex = gce.TransparentColourIndex;

			ImageDescriptor imageDescriptor = new ImageDescriptor( inputStream, 
			                                                       XmlDebugging );
			WriteDebugXmlNode( imageDescriptor.DebugXmlReader );
			
			#region determine the colour table to use for this frame
			Color backgroundColour = Color.FromArgb( 0 ); // TODO: is this the right background colour?
			// TODO: use backgroundColourIndex from the logical screen descriptor?
			ColourTable activeColourTable;
			if( imageDescriptor.HasLocalColourTable ) 
			{
				_localColourTable 
					= new ColourTable( inputStream,
					                   imageDescriptor.LocalColourTableSize, 
					                   XmlDebugging );
				WriteDebugXmlNode( _localColourTable.DebugXmlReader );
				activeColourTable = _localColourTable; // make local table active
			} 
			else 
			{
				if( gct == null )
				{
					// We have neither local nor global colour table, so we
					// won't be able to decode this frame.
					Bitmap emptyBitmap = new Bitmap( lsd.LogicalScreenSize.Width, 
					                                 lsd.LogicalScreenSize.Height );
					_image = emptyBitmap;
					_delay = gce.DelayTime;
					SetStatus( ErrorState.FrameHasNoColourTable, "" );
					return;
				}
				activeColourTable = gct; // make global table active
				if( lsd.BackgroundColourIndex == transparentColourIndex )
				{
					backgroundColour = Color.FromArgb( 0 );
				}
			}
			#endregion

			// decode pixel data
			int pixelCount = imageDescriptor.Size.Width * imageDescriptor.Size.Height;
			TableBasedImageData indexedPixels 
				= new TableBasedImageData( inputStream, pixelCount, XmlDebugging );
			WriteDebugXmlNode( indexedPixels.DebugXmlReader );
			
			if( indexedPixels.Pixels.Count == 0 )
			{
				// TESTME: constructor - indexedPixels.Pixels.Count == 0
				Bitmap emptyBitmap = new Bitmap( lsd.LogicalScreenSize.Width, 
				                                 lsd.LogicalScreenSize.Height );
				_image = emptyBitmap;
				_delay = gce.DelayTime;
				SetStatus( ErrorState.FrameHasNoImageData, "" );
				WriteDebugXmlFinish();
				return;
			}
			
			// Skip any remaining blocks up to the next block terminator (in
			// case there is any surplus data before the next frame)
			SkipBlocks( inputStream );

			_indexedPixels = indexedPixels;

			_extension = gce;
			if( gce != null )
			{
				_delay = gce.DelayTime;
			}
			_imageDescriptor = imageDescriptor;
			_backgroundColour = backgroundColour;
			GifComponentStatus status;
			_image = CreateBitmap( indexedPixels, 
			                       lsd,
			                       imageDescriptor,
			                       activeColourTable,
			                       gce,
			                       previousFrame,
			                       previousFrameBut1,
			                       out status );
			
			WriteDebugXmlFinish();
		}
		#endregion

		#endregion
		
		#region properties
		
		#region read/write properties
		
		#region Delay property
		/// <summary>
		/// Gets and sets the delay in hundredths of a second before showing 
		/// the next frame.
		/// </summary>
		[Description( "The delay in hundredths of a second before showing " +
		              "the next frame in the animation" )]
		public int Delay
		{
			get { return _delay; }
			set { _delay = value; }
		}
		#endregion
		
		#region BackgroundColour property
		/// <summary>
		/// Gets and sets the background colour of the current frame
		/// </summary>
		[Description( "The background colour for this frame." )]
		public Color BackgroundColour
		{
			get { return _backgroundColour; }
			set { _backgroundColour = value; }
		}
		#endregion
		
		#region ExpectsUserInput property
		/// <summary>
		/// Gets a flag indicating whether the device displaying the animation
		/// should wait for user input (e.g. a mouse click or key press) before
		/// displaying the next frame.
		/// </summary>
		/// <remarks>
		/// This is actually a property of the graphic control extension.
		/// </remarks>
		/// <exception cref="InvalidOperationException">
		/// An attempt was made to set this property for a GifFrame which was
		/// created by a GifDecoder.
		/// </exception>
		[Description( "Gets a flag indicating whether the device displaying " +
		              "the animation should wait for user input (e.g. a mouse " +
		              "click or key press) before displaying the next frame." )]
		public bool ExpectsUserInput
		{
			get 
			{ 
				if( _extension == null )
				{
					return _expectsUserInput; 
				}
				else
				{
					return _extension.ExpectsUserInput;
				}
			}
			set 
			{ 
				if( _extension == null )
				{
					_expectsUserInput = value;
				}
				else
				{
					string message
						= "This GifFrame was returned by a GifDecoder so this "
						+ "property is read-only";
					throw new InvalidOperationException( message );
				}
			}
		}
		#endregion
		
		#region Position property
		/// <summary>
		/// Gets and sets the position of this frame's image within the logical
		/// screen.
		/// </summary>
		/// <remarks>
		/// This is actually a property of the image descriptor.
		/// </remarks>
		/// <exception cref="InvalidOperationException">
		/// An attempt was made to set this property for a GifFrame which was
		/// created by a GifDecoder.
		/// </exception>
		[Description( "Gets and sets the position of this frame's image " +
		              "within the logical screen." )]
		public Point Position
		{
			get
			{
				if( _imageDescriptor == null )
				{
					return _position;
				}
				else
				{
					return _imageDescriptor.Position;
				}
			}
			set
			{
				if( _imageDescriptor == null )
				{
					_position = value;
				}
				else
				{
					string message
						= "This GifFrame was returned by a GifDecoder so this "
						+ "property is read-only";
					throw new InvalidOperationException( message );
				}
			}
		}
		#endregion
		
		#region Palette property
		/// <summary>
		/// Gets and sets a <see cref="Palette"/> to be used as the frame's
		/// local colour table.
		/// </summary>
		[SuppressMessage("Microsoft.Usage", 
		                 "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public Palette Palette
		{
			get { return _palette; }
			set 
			{ 
				if( value == null )
				{
					throw new ArgumentNullException( "value" );
				}
				// TESTME: set_Palette not null
				value.Validate();
				_palette = value; 
			}
		}
		#endregion
		
		#endregion
		
		#region read-only properties
		
		#region TheImage property
		/// <summary>
		/// Gets the image held in this frame.
		/// </summary>
		[Description( "The image held in this frame" )]
		[Category( "Set by decoder" )]
		public Image TheImage
		{
			get { return _image; }
		}
		#endregion
		
		#region LocalColourTable property
		/// <summary>
		/// Gets the local colour table for this frame.
		/// </summary>
		[Description( "The local colour table for this frame" )]
		[Category( "Set by decoder" )]
		public ColourTable LocalColourTable
		{
			get { return _localColourTable; }
		}
		#endregion

		#region GraphicControlExtension property
		/// <summary>
		/// Gets the graphic control extension which precedes this image.
		/// </summary>
		[Description( "The graphic control extension which precedes this image." )]
		[Category( "Set by decoder" )]
		public GraphicControlExtension GraphicControlExtension
		{
			get { return _extension; }
		}
		#endregion
		
		#region ImageDescriptor property
		/// <summary>
		/// Gets the image descriptor for this frame.
		/// </summary>
		[Category( "Set by decoder" )]
		[Description( "The image descriptor for this frame. This contains the " +
		              "size and position of the image, and flags indicating " +
		              "whether the colour table is global or local, whether " +
		              "it is sorted, and whether the image is interlaced." )]
		public ImageDescriptor ImageDescriptor
		{
			get { return _imageDescriptor; }
		}
		#endregion

		#region IndexedPixels property
		/// <summary>
		/// Gets the table-based image data containing the indices within the
		/// active colour table of the colours of each of the pixels in the
		/// frame.
		/// </summary>
		[Category( "Set by decoder" )]
		[Description( "Gets the table-based image data containing the " + 
		              "indices within the active colour table of the colours " + 
		              "of each of the pixels in the frame." )]
		public TableBasedImageData IndexedPixels
		{
			get { return _indexedPixels; }
		}
		#endregion
		
		#endregion
		
		#endregion
		
		#region public override WriteToStream method
		/// <summary>
		/// Writes this component to the supplied output stream.
		/// </summary>
		/// <param name="outputStream">
		/// The output stream to write to.
		/// </param>
		public override void WriteToStream( Stream outputStream )
		{
			throw new NotImplementedException();
		}
		#endregion
		
		#region private methods
		
		#region private static CreateBitmap( GifDecoder, ImageDescriptor, ColourTable, bool ) method
		/// <summary>
		/// Sets the pixels of the decoded image.
		/// </summary>
		/// <param name="imageData">
		/// Table based image data containing the indices within the active
		/// colour table of the colours of the pixels in this frame.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for the GIF stream.
		/// </param>
		/// <param name="id">
		/// The image descriptor for this frame.
		/// </param>
		/// <param name="activeColourTable">
		/// The colour table to use with this frame - either the global colour
		/// table or a local colour table.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension, if any, which precedes this image in
		/// the input stream.
		/// </param>
		/// <param name="previousFrame">
		/// The frame which precedes this one in the GIF stream, if present.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which precedes the frame before this one in the GIF stream,
		/// if present.
		/// </param>
		/// <param name="status">
		/// GifComponentStatus containing any errors which occurred during the
		/// creation of the bitmap.
		/// </param>
		private static Bitmap CreateBitmap( TableBasedImageData imageData,
		                                    LogicalScreenDescriptor lsd,
		                                    ImageDescriptor id,
		                                    ColourTable activeColourTable,
		                                    GraphicControlExtension gce,
		                                    GifFrame previousFrame,
		                                    GifFrame previousFrameBut1, 
		                                    out GifComponentStatus status )
		{
			status = new GifComponentStatus( ErrorState.Ok, "" );
			Color[] pixelsForThisFrame = new Color[lsd.LogicalScreenSize.Width 
			                                       * lsd.LogicalScreenSize.Height];
			
			Bitmap baseImage = GetBaseImage( previousFrame, 
			                                 previousFrameBut1, 
			                                 lsd, 
			                                 gce, 
			                                 activeColourTable );

			// copy each source line to the appropriate place in the destination
			int pass = 1;
			int interlaceRowIncrement = 8;
			int interlaceRowNumber = 0; // the row of pixels we're currently 
										// setting in an interlaced image.
			for( int i = 0; i < id.Size.Height; i++)  
			{
				int pixelRowNumber = i;
				if( id.IsInterlaced ) 
				{
					#region work out the pixel row we're setting for an interlaced image
					if( interlaceRowNumber >= id.Size.Height ) 
					{
						pass++;
						switch( pass )
						{
							case 2 :
								interlaceRowNumber = 4;
								break;
							case 3 :
								interlaceRowNumber = 2;
								interlaceRowIncrement = 4;
								break;
							case 4 :
								interlaceRowNumber = 1;
								interlaceRowIncrement = 2;
								break;
						}
					}
					#endregion
					pixelRowNumber = interlaceRowNumber;
					interlaceRowNumber += interlaceRowIncrement;
				}
				
				// Colour in the pixels for this row
				pixelRowNumber += id.Position.Y;
				if( pixelRowNumber < lsd.LogicalScreenSize.Height ) 
				{
					int k = pixelRowNumber * lsd.LogicalScreenSize.Width;
					int dx = k + id.Position.X; // start of line in dest
					int dlim = dx + id.Size.Width; // end of dest line
					if( (k + lsd.LogicalScreenSize.Width) < dlim ) 
					{
						// TESTME: CreateBitmap - past dest edge
						dlim = k + lsd.LogicalScreenSize.Width; // past dest edge
					}
					int sx = i * id.Size.Width; // start of line in source
					while (dx < dlim) 
					{
						// map color and insert in destination
						int indexInColourTable = (int) imageData.Pixels[sx++];
						// Set this pixel's colour if its index isn't the 
						// transparent colour index, or if this frame doesn't
						// have a transparent colour.
						Color c;
						if( gce.HasTransparentColour && indexInColourTable == gce.TransparentColourIndex )
						{
							c = Color.Empty; // transparent pixel
						}
						else
						{
							if( indexInColourTable < activeColourTable.Length )
							{
								c = activeColourTable[indexInColourTable];
							}
							else
							{
								// TESTME: CreateBitmap - BadColourIndex
								c = Color.Black;
								string message 
									= "Colour index: "
									+ indexInColourTable
									+ ", colour table length: "
									+ activeColourTable.Length
									+ " (" + dx + "," + pixelRowNumber + ")";
								status = new GifComponentStatus( ErrorState.BadColourIndex, 
								                                 message );
							}
						}
						pixelsForThisFrame[dx] = c;
						dx++;
					}
				}
			}
			return CreateBitmap( baseImage, pixelsForThisFrame );
		}
		#endregion
		
		#region private static GetBaseImage method
		/// <summary>
		/// Gets the base image for this frame. This will be overpainted with
		/// the pixels for this frame, where they are not transparent.
		/// </summary>
		/// <param name="previousFrame">
		/// The frame which preceded this frame in the GIF stream.
		/// Null if this is the first frame in the stream.
		/// </param>
		/// <param name="previousFrameBut1">
		/// The frame which preceded the previous frame in the GIF stream.
		/// Null if this is the first or seond frame in the stream.
		/// </param>
		/// <param name="lsd">
		/// The logical screen descriptor for this GIF stream.
		/// </param>
		/// <param name="gce">
		/// The graphic control extension for this frame.
		/// </param>
		/// <param name="act">
		/// The active colour table for this frame.
		/// </param>
		/// <returns></returns>
		private static Bitmap GetBaseImage( GifFrame previousFrame, 
		                                    GifFrame previousFrameBut1, 
		                                    LogicalScreenDescriptor lsd, 
		                                    GraphicControlExtension gce, 
		                                    ColourTable act )
		{
			#region Get the disposal method of the previous frame read from the GIF stream
			DisposalMethod previousDisposalMethod;
			if( previousFrame == null )
			{
				previousDisposalMethod = DisposalMethod.NotSpecified;
			}
			else
			{
				previousDisposalMethod = previousFrame.GraphicControlExtension.DisposalMethod;
			}
			#endregion

			Bitmap baseImage;
			int width = lsd.LogicalScreenSize.Width;
			int height = lsd.LogicalScreenSize.Height;
			
			#region paint baseImage
			switch( previousDisposalMethod )
			{
				case DisposalMethod.DoNotDispose:
					// pre-populate image with previous frame
					baseImage = new Bitmap( previousFrame.TheImage );
					break;
					
				case DisposalMethod.RestoreToBackgroundColour:
					// pre-populate image with background colour
					Color backgroundColour ;
					if( lsd.BackgroundColourIndex == gce.TransparentColourIndex )
					{
						backgroundColour = Color.Empty;
					}
					else
					{
						// TESTME: background colour index different to transparent colour
						backgroundColour = act[lsd.BackgroundColourIndex];
					}
					baseImage = new Bitmap( width, height );
					for( int y = 0; y < height; y++ )
					{
						for( int x = 0; x < height; x++ )
						{
							baseImage.SetPixel( x, y, backgroundColour );
						}
					}
					break;
					
				case DisposalMethod.RestoreToPrevious:
					// pre-populate image with previous frame but 1
					// TESTME: DisposalMethod.RestoreToPrevious
					baseImage = new Bitmap( previousFrameBut1.TheImage );
					break;
					
				default: // DisposalMethod.NotSpecified
					if( previousFrame == null )
					{
						// this is the first frame so start with an empty bitmap
						baseImage = new Bitmap( width, height );
					}
					else
					{
						// pre-populate image with previous frame
						// TESTME: DisposalMethod.NotSpecified on 2nd frame or later
						baseImage = new Bitmap( previousFrame.TheImage );
					}
					break;
			}
			#endregion

			return baseImage;
		}
		#endregion

		#region private static CreateBitmap( Bitmap, Color[] ) method
		/// <summary>
		/// Creates and returns a Bitmap of the supplied size composed of pixels
		/// of the supplied colours, working left to right and then top to 
		/// bottom.
		/// </summary>
		/// <param name="baseImage">
		/// The image to start with; this is overpainted with the supplied 
		/// pixels where they are not transparent.
		/// </param>
		/// <param name="pixels">
		/// An array of the colours of the pixels for the bitmap to be created.
		/// </param>
		private static Bitmap CreateBitmap( Bitmap baseImage, Color[] pixels )
		{
			int count = 0;
			for( int th = 0; th < baseImage.Height; th++ )
			{
				for( int tw = 0; tw < baseImage.Width; tw++ )
				{
					if( pixels[count] != Color.Empty )
					{
						// then it's not a transparent pixel
						baseImage.SetPixel( tw, th, pixels[count] );
					}
					count++;
				}
			}
			return baseImage;
		}
		#endregion

		#endregion

	}
}
