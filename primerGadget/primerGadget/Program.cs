using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace primerGadget
{

    public partial class Program
    {
        int numero = 0;
        GT.Timer timer = new GT.Timer(1500,GT.Timer.BehaviorType.RunOnce); // every second (1000ms)
        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
           
             timer.Tick += timer_Tick;       
          
            camera.CameraConnected += camera_CameraConnected;
            camera.BitmapStreamed += camera_BitmapStreamed;
            button.ButtonPressed += button_ButtonPressed;
            camera.PictureCaptured += camera_PictureCaptured;
            sdCard.Mounted += sdCard_Mounted;
        

        }

        void sdCard_Mounted(SDCard sender, GT.StorageDevice device)
        {
            Debug.Print("sd insertada");
        }

        void timer_Tick(GT.Timer timer)
        {
            Debug.Print("camara Started after 1500ms");
            camera.StartStreaming();
        }

  

        void camera_PictureCaptured(Camera sender, GT.Picture e)
        {
            Debug.Print("imagen capturada");
            button.TurnLedOn();
            sdCard.StorageDevice.WriteFile("picture.bmp", e.PictureData);
               
                 Debug.Print("imagen guardada");
                button.TurnLedOff();
                timer.Start();
             


        }

        private void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
            Debug.Print("boton presionado");
            camera.StopStreaming();
            camera.TakePicture();
           

           
        }

      

        void camera_CameraConnected(Camera sender, EventArgs e)
        {
            Debug.Print("camara Started");
            camera.StartStreaming();
            
        }
      
        private void camera_BitmapStreamed(Camera sender, Bitmap e)
        {

            displayT35.SimpleGraphics.DisplayImage(e, 0, 0);
                
         
           // camera.StopStreaming();
        }

        
    }
}
