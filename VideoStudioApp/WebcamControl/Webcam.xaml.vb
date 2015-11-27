﻿Imports Microsoft.Expression.Encoder
Imports Microsoft.Expression.Encoder.Devices
Imports Microsoft.Expression.Encoder.Live
Imports Microsoft.Expression.Encoder.Profiles
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.ComponentModel

Public Class Webcam
    Implements IDisposable

#Region "VideoFileFormat dependency property"
    ''' <summary>
    ''' Gets the video format in which the webcam video will be saved.
    ''' </summary>
    Public ReadOnly Property VideoFileFormat As String
        Get
            Return GetValue(Webcam.VideoFileFormatProperty)
        End Get
    End Property

    Private Shared ReadOnly VideoFileFormatPropertyKey As DependencyPropertyKey =
        DependencyProperty.RegisterReadOnly("VideoFileFormat", GetType(String), GetType(Webcam), New PropertyMetadata(".avi"))

    Public Shared ReadOnly VideoFileFormatProperty As DependencyProperty = VideoFileFormatPropertyKey.DependencyProperty
#End Region

#Region "SnapshotFormat dependency property"
    ''' <summary>
    ''' Gets or Sets format used when saving snapshot of webcam image.
    ''' </summary>    
    Public Property SnapshotFormat() As ImageFormat
        Get
            Return CType(GetValue(SnapshotFormatProperty), ImageFormat)
        End Get
        Set(ByVal value As ImageFormat)
            SetValue(SnapshotFormatProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SnapshotFormatProperty As DependencyProperty =
        DependencyProperty.Register("SnapshotFormat", GetType(ImageFormat), GetType(Webcam), New PropertyMetadata(ImageFormat.Jpeg))
#End Region

#Region "VideoDevice dependency property"
    ''' <summary>
    ''' Gets or Sets the name of the video device to be used.
    ''' </summary>    
    Public Property VideoDevice() As EncoderDevice
        Get
            Return CType(GetValue(VideoDeviceProperty), EncoderDevice)
        End Get
        Set(ByVal value As EncoderDevice)
            SetValue(VideoDeviceProperty, value)
        End Set
    End Property

    Public Shared ReadOnly VideoDeviceProperty As DependencyProperty =
        DependencyProperty.Register("VideoDevice", GetType(EncoderDevice), GetType(Webcam),
                                    New PropertyMetadata(New PropertyChangedCallback(AddressOf VideoDeviceChange)))

    Private _videoDevice As EncoderDevice
    Private Shared Sub VideoDeviceChange(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim device As EncoderDevice = CType(e.NewValue, EncoderDevice)
        Dim devices As IEnumerable(Of EncoderDevice) =
            EncoderDevices.FindDevices(EncoderDeviceType.Video).Where(Function(dv) dv.Name = device.Name)

        If (devices.Count <> 0) Then
            Dim src As Webcam = CType(source, Webcam)
            src._videoDevice = devices.First
            If (src.isPreviewing) Then
                src.StartPreview()
            End If
        End If
    End Sub
#End Region

#Region "AudioDevice dependency property"
    ''' <summary>
    ''' Gets or Sets the name of the audio device to be used.
    ''' </summary>    
    Public Property AudioDevice() As EncoderDevice
        Get
            Return CType(GetValue(AudioDeviceProperty), EncoderDevice)
        End Get
        Set(ByVal value As EncoderDevice)
            SetValue(AudioDeviceProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AudioDeviceProperty As DependencyProperty =
        DependencyProperty.Register("AudioDevice", GetType(EncoderDevice), GetType(Webcam),
                                    New PropertyMetadata(New PropertyChangedCallback(AddressOf AudioDeviceChange)))

    Private _audioDevice As EncoderDevice
    Private Shared Sub AudioDeviceChange(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim device As EncoderDevice = CType(e.NewValue, EncoderDevice)
        Dim devices As IEnumerable(Of EncoderDevice) =
            EncoderDevices.FindDevices(EncoderDeviceType.Audio).Where(Function(dv) dv.Name = device.Name)

        If (devices.Count <> 0) Then
            Dim src As Webcam = CType(source, Webcam)
            src._audioDevice = devices.First
            If (src.isPreviewing) Then
                src.StartPreview()
            End If
        End If
    End Sub
#End Region

#Region "VideoDirectory dependency property"
    ''' <summary>
    ''' Gets or Sets the folder where the recorded webcam video will be saved.
    ''' </summary>    
    Public Property VideoDirectory() As String
        Get
            Return CType(GetValue(VideoDirectoryProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(VideoDirectoryProperty, value)
        End Set
    End Property

    Public Shared ReadOnly VideoDirectoryProperty As DependencyProperty =
        DependencyProperty.Register("VideoDirectory", GetType(String), GetType(Webcam), New PropertyMetadata(Nothing))
#End Region

#Region "ImageDirectory dependency property"
    ''' <summary>
    ''' Gets or Sets the folder where video snapshot will be saved.
    ''' </summary>    
    Public Property ImageDirectory() As String
        Get
            Return CType(GetValue(ImageDirectoryProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(ImageDirectoryProperty, value)
        End Set
    End Property

    Public Shared ImageDirectoryProperty As DependencyProperty =
        DependencyProperty.Register("ImageDirectory", GetType(String), GetType(Webcam), New PropertyMetadata(Nothing))
#End Region

#Region "FrameRate dependency property"
    ''' <summary>
    ''' Gets or sets the frame rate, in frames per second. Default value is 15.
    ''' </summary>    
    Public Property FrameRate As Integer
        Get
            Return CType(GetValue(FrameRateProperty), Integer)
        End Get
        Set(value As Integer)
            SetValue(FrameRateProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FrameRateProperty As DependencyProperty =
        DependencyProperty.Register("FrameRate", GetType(Integer), GetType(Webcam),
                                    New PropertyMetadata(15, New PropertyChangedCallback(AddressOf FrameRateChange)))

    Private Shared Sub FrameRateChange(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim src As Webcam = CType(source, Webcam)
        If (src.isPreviewing) Then
            src.StartPreview()
        End If
    End Sub
#End Region

#Region "FrameSize dependency property"
    ''' <summary>
    ''' Gets or sets the size of the video profile. Default is 320x240.
    ''' </summary>    
    Public Property FrameSize As Size
        Get
            Return CType(GetValue(FrameSizeProperty), Size)
        End Get
        Set(value As Size)
            SetValue(FrameSizeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FrameSizeProperty As DependencyProperty =
        DependencyProperty.Register("FrameSize", GetType(Size), GetType(Webcam),
                                    New PropertyMetadata(New Size(320, 240), New PropertyChangedCallback(AddressOf FrameSizeChange)))

    Private Shared Sub FrameSizeChange(ByVal source As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim src As Webcam = CType(source, Webcam)
        If (src.isPreviewing) Then
            src.StartPreview()
        End If
    End Sub
#End Region

#Region "IsRecording dependency property"
    ''' <summary>
    ''' Gets a value indicating whether video recording is taking place.
    ''' </summary>
    Public ReadOnly Property IsRecording As Boolean
        Get
            Return GetValue(Webcam.IsRecordingProperty)
        End Get
    End Property

    Private Shared ReadOnly IsRecordingPropertyKey As DependencyPropertyKey =
        DependencyProperty.RegisterReadOnly("IsRecording", GetType(Boolean), GetType(Webcam), New PropertyMetadata(Nothing))

    Public Shared ReadOnly IsRecordingProperty As DependencyProperty = IsRecordingPropertyKey.DependencyProperty
#End Region



#Region "Imagen Marca de Agua dependency property"
    ''' <summary>
    ''' Gets or Sets the folder where video snapshot will be saved.
    ''' </summary>    
    Public Property ImagenMarcaAgua() As String
        Get
            Return CType(GetValue(ImagenMarcaAguaProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(ImagenMarcaAguaProperty, value)
        End Set
    End Property

    Public Shared ImagenMarcaAguaProperty As DependencyProperty =
        DependencyProperty.Register("ImagenMarcaAgua", GetType(String), GetType(Webcam), New PropertyMetadata(Nothing))
#End Region

#Region "Nombre Archivo video dependency property"
    ''' <summary>
    ''' Gets or Sets the folder where video snapshot will be saved.
    ''' </summary>    
    Public Property NombreVideo() As String
        Get
            Return CType(GetValue(NombreVideoProperty), String)
        End Get
        Set(ByVal value As String)
            SetValue(NombreVideoProperty, value)
        End Set
    End Property

    Public Shared NombreVideoProperty As DependencyProperty =
        DependencyProperty.Register("NombreVideo", GetType(String), GetType(Webcam), New PropertyMetadata(Nothing))
#End Region


#Region "Visibilidad w host  Marca de agua video dependency property"
    ''' <summary>
    ''' Gets or Sets the folder where video snapshot will be saved.
    ''' </summary>    
    Public Property WfVisibilityImg() As Visibility
        Get
            Return CType(GetValue(WfVisibilityImgProperty), Visibility)
        End Get
        Set(ByVal value As Visibility)
            SetValue(WfVisibilityImgProperty, value)
        End Set
    End Property

    Public Shared WfVisibilityImgProperty As DependencyProperty =
        DependencyProperty.Register("WfVisibilityImg", GetType(Visibility), GetType(Webcam), New PropertyMetadata(Nothing))
#End Region




    Private deviceSource As LiveDeviceSource
    Private job As LiveJob
    Private isPreviewing As Boolean

    ''' <summary>
    ''' Displays webcam video.
    ''' </summary>
    Public Sub StartPreview()
        Try
            If (_videoDevice IsNot Nothing) Then
                StopRecording()
                StopPreview()


                wfImg.Visibility = WfVisibilityImg
                imgMarcaAgua.Image = Image.FromFile(ImagenMarcaAgua)

                canvasContent.Visibility = Windows.Visibility.Visible

                job = New LiveJob
                Dim frameDuration As Long = CLng(FrameRate * Math.Pow(10, 7))

                deviceSource = job.AddDeviceSource(_videoDevice, _audioDevice)
                deviceSource.PickBestVideoFormat(FrameSize, frameDuration)
                deviceSource.PreviewWindow = New PreviewWindow(New HandleRef(WebcamPanel, WebcamPanel.Handle))

                job.OutputFormat.VideoProfile = New AdvancedVC1VideoProfile
                job.OutputFormat.VideoProfile.Size = FrameSize
                job.OutputFormat.VideoProfile.FrameRate = FrameRate

                job.ActivateSource(deviceSource)

                isPreviewing = True



            End If
        Catch ex As Microsoft.Expression.Encoder.SystemErrorException
            Throw New Microsoft.Expression.Encoder.SystemErrorException
        End Try
    End Sub

    ''' <summary>
    ''' Stops the display of webcam video.
    ''' </summary>
    Public Sub StopPreview()
        If (job IsNot Nothing) Then
            StopRecording()
            deviceSource = Nothing
            job.Dispose()
            isPreviewing = False
        End If
    End Sub

    Private Function TimeStamp() As String
        Dim ts As String = DateTime.Now.ToString
        ts = ts.Replace("/", "-")
        ts = ts.Replace(":", ".")
        Return ts
    End Function

    ''' <summary>
    ''' Starts the recording of webcam images to a video file.
    ''' </summary>
    Public Sub StartRecording()
        Dim vidDir As String = CType(GetValue(VideoDirectoryProperty), String)

        If (vidDir = String.Empty) Then
            Throw New DirectoryNotFoundException("Video directory has not been specified")
            Exit Sub
        ElseIf (Not Directory.Exists(vidDir)) Then
            Throw New DirectoryNotFoundException("The specified video directory does not exist")
            Exit Sub
        End If

        If (job IsNot Nothing AndAlso isPreviewing) Then
            StopRecording()

            Dim filePath As String = Path.Combine(vidDir, NombreVideo & ".avi")
            Dim fileArcvFormat As New FileArchivePublishFormat(filePath)

            job.PublishFormats.Clear()

            job.PublishFormats.Add(fileArcvFormat)
            job.StartEncoding()

            SetValue(IsRecordingPropertyKey, True)
        End If
    End Sub

    ''' <summary>
    ''' Stops the recording of webcam video.
    ''' </summary>
    Public Sub StopRecording()
        Dim recording As Boolean = CType(GetValue(IsRecordingProperty), Boolean)
        If (job IsNot Nothing AndAlso recording) Then
            job.StopEncoding()
            SetValue(IsRecordingPropertyKey, False)
        End If
    End Sub

    ''' <summary>
    ''' Takes a snapshot of an webcam image.
    ''' The size of the image will be equal to the size of the control.
    ''' </summary>
    Public Sub TakeSnapshot()
        Dim imgDir As String = CType(GetValue(ImageDirectoryProperty), String)

        If (imgDir = String.Empty) Then
            Throw New DirectoryNotFoundException("Image directory has not been specified")
            Exit Sub
        ElseIf (Not Directory.Exists(imgDir)) Then
            Throw New DirectoryNotFoundException("The specified image directory does not exist")
            Exit Sub
        End If

        If (job IsNot Nothing AndAlso isPreviewing) Then
            Dim panelWidth As Integer = WebcamPanel.Width
            Dim panelHeight As Integer = WebcamPanel.Height
            Dim imgFormat As ImageFormat = CType(GetValue(SnapshotFormatProperty), ImageFormat)
            Dim filePath As String = Path.Combine(imgDir, "Snapshot " & TimeStamp() & "." & imgFormat.ToString)
            Dim pnt As Point = WebcamPanel.PointToScreen(New Point(WebcamPanel.ClientRectangle.X, WebcamPanel.ClientRectangle.Y))

            Using bmp As New Bitmap(panelWidth, panelHeight)
                Using grx As Graphics = Graphics.FromImage(bmp)
                    grx.CopyFromScreen(pnt, Point.Empty, New Size(panelWidth, panelHeight))
                End Using
                bmp.Save(filePath, imgFormat)
            End Using
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If (deviceSource IsNot Nothing) Then
                    deviceSource.PreviewWindow.Dispose()
                    deviceSource.PreviewWindow = Nothing
                    deviceSource.Dispose()
                    deviceSource = Nothing
                End If

                If (job IsNot Nothing) Then
                    job.Dispose()
                    job = Nothing
                End If

                WinFormsHost.Dispose()
                WinFormsHost = Nothing

                WebcamPanel.Dispose()
                WebcamPanel = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Private Sub Webcam_SizeChanged(sender As Object, e As SizeChangedEventArgs) Handles Me.SizeChanged
        If (deviceSource IsNot Nothing) Then
            deviceSource.PreviewWindow.SetSize(New Size(CInt(Me.ActualWidth), CInt(Me.ActualHeight)))
        End If
    End Sub

    Private Sub Webcam_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        Dispose()
    End Sub

    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()


    End Sub
End Class
