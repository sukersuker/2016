using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace dllESDFileAP
{
    public class _Location
    {
        public string _folderpath;
        public string _filepath;

        public dllESDRegister._Execution LoadFile_Location(dllESDRegister._Object _Register)
        {
            string[] _sfileCount = new string[100];
            dllESDRegister._Execution _execution_result = new dllESDRegister._Execution();
            string[] _Item;

            if (File.Exists("Location.cfg"))
            {
                _sfileCount = File.ReadAllLines("Location.cfg");

                for (int i = 0; i < 5; i++)
                {
                    if (_sfileCount[i].Contains("[Location]"))
                    {
                        for (int _x = 0; _x < 0x40; _x++)
                        {
                            _Item = _sfileCount[++i].Split('~', '=', ':');
                            _Register.ESDTSystem.ESDLocation[_x] = _Item[2];
                            _Register.ESDTSystem.ESDDisable[_x] = _Item[3];
                         }

                     }
                 }
             }
            return _execution_result.Clone() as dllESDRegister._Execution;
        }

        public dllESDRegister._Execution SaveFile_Location(dllESDRegister._Object _Register)
        {
            string[] _sfileCount = new string[100];
            string _temp = "";
            int _Countline = 0;
            dllESDRegister._Execution _execution_result = new dllESDRegister._Execution();

            _sfileCount[_Countline++] = "[Location]";

            for (int _x = 0; _x < 0x40; _x++)
            {
                _temp = Convert.ToString(_x, 16).PadLeft(2, '0').ToUpper();
                _sfileCount[_Countline++] = _temp + "~" + "ESDLocation_" + _temp + "=" + _Register.ESDTSystem.ESDLocation[_x] + ":" + _Register.ESDTSystem.ESDDisable[_x];
            }

            File.WriteAllLines("Location.cfg", _sfileCount);
            return _execution_result.Clone() as dllESDRegister._Execution;
        }

        public dllESDRegister._Execution SaveLogFile(dllESDRegister._Object _Register,string _address, string _name, string _msg,string _status)
        {
            dllESDRegister._Execution _execution_result = new dllESDRegister._Execution();
            string _temp_msg="";
            _folderpath = "LogFile\\"+ Convert.ToString(System.DateTime.Now.Year) + Convert.ToString(System.DateTime.Now.Month).PadLeft(2,'0') + Convert.ToString(System.DateTime.Now.Day).PadLeft(2,'0');
            _filepath = Directory.GetCurrentDirectory()+ "\\"+_folderpath + ".cfg";
            string _time = DateTime.Now.Hour.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Minute.ToString().PadLeft(2, '0') + ":" + DateTime.Now.Second.ToString().PadLeft(2, '0');
            _temp_msg = _time+"\t\t"+_address+"\t\t"+_name+"\t\t"+_msg +"\t\t"+_status+ "\r\n";
            /*if (!Directory.Exists(_folderpath))
                Directory.CreateDirectory(_folderpath);*/
            if (!File.Exists(_filepath))
            {
                _temp_msg ="時間    \t\t"+"地址\t\t"+"站名\t\t"+"狀態信息\t\t" +"狀態\t\t" + "\r\n";
                File.AppendAllText(_filepath, _temp_msg, Encoding.Default);
            }
            else
                File.AppendAllText(_filepath, _temp_msg, Encoding.Default);

            return _execution_result.Clone() as dllESDRegister._Execution;
        }
       
    }
}