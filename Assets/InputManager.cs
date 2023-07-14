using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using static UnityEngine.Rendering.DebugUI;

public class InputManager : MonoBehaviour
{


    public Vector2 movementInput;
    public float zForward;
    public bool rotaryclick;
    public bool spaceDown;
    SerialPort sp = new SerialPort("COM3", 115200);
    bool isStreaming = false;
    public static InputManager instance;
    int lastrot;

    class data
    {
       public Vector2 joyval;
       public int rotary;
        public bool rotaryclick;
    }
    private void Awake()
    {
        if(instance == null)
        instance = this;
        else 

        Destroy(this);
        
    }
    void OpenConnection()
    {
        isStreaming = true;
        sp.ReadTimeout = 100;
        sp.Open();
    }
    void Close()
    {
        sp.Close();
    }
    data ReadSerialPort(out bool read,int timeout = 500 )
    {

        data message = new data();
        if (!sp.IsOpen)
        {
            read = false;
            return message;
        };
        sp.ReadTimeout = timeout;
        if (sp.BytesToRead < 3)
        {
            read = false;
            return message;
        }
        try
        {
           
                string data = sp.ReadLine();

          
                string[] values = data.Split(',');

            
                print( "Joystick X: " + values[0] + "Joystick Y: " + values[1] + "Encoder Value: " + values[2]+"btn Value: " + values[3]);
            
                read = true;
                message.joyval.x=(float)Convert.ToInt16( values[1])/517;
                message.joyval.y = (float)Convert.ToInt16(values[0]) / 517;
                message.rotary = Convert.ToInt16(values[2]);
                message.rotaryclick= Convert.ToInt16(values[3])==0;
            
            return message;
        }
        catch (TimeoutException)
        {
                read = false;
                return message;
        }

        
        read = false;
        return message;
  
    }
    // Start is called before the first frame update
    void Start()
    {
        OpenConnection();
    }
    bool lastclick;
    private void Update()
    {
        rotaryclick = false;
    
        bool read;
        data value = ReadSerialPort(out read,500);
        if (value != null && read)
        {
            movementInput = value.joyval;
            zForward = value.rotary - lastrot;
            lastrot = value.rotary;
        
            if (lastclick != value.rotaryclick && value.rotaryclick == true)
            {
                rotaryclick = value.rotaryclick;
            
            }
            lastclick = value.rotaryclick;
        }
        else
        {
            //ReadInputs();
        }

        if (_buzz)
        {
            sp.Write(true ? "1" : "0");
            _buzz = false;
        }
    }
    bool _buzz;
    public void buzz()
    {

        _buzz = true;
     
    }
 
  

 
    public void ReadInputs()
    {

        movementInput = Vector2.zero;
       // movementInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movementInput.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementInput.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementInput.x = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementInput.x = -1;
        }
        spaceDown = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown=true;
        }

    }
    private void OnDisable()
    {
        Close();
    }
}
