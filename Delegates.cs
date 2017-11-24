using System;

namespace SysLog
{
    public class Del
    {
        public delegate void Deleg1(string email);
        public event Deleg1 Loginsup;
        
        public delegate void Deleg2(string email);
        public event Deleg1 Logoutsup;
       
    }
}