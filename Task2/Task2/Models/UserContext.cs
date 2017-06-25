using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Task2.Models
{
    // Fake Context
    public class UserContext : IDisposable
    {
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public List<User> Users { get; set; }

        public UserContext()
        {
            Users = new List<User>();
            User HardCodedUsers = new User();
            // User 1
            HardCodedUsers.ID = 1;
            HardCodedUsers.UserName = "misha";
            HardCodedUsers.Password = "7c4a8d09ca3762af61e59520943dc26494f8941b";
            Users.Add(HardCodedUsers);
        }

        public bool SaveChanges()
        {
            return true;
        }

        public void Dispose()
        {
            if (disposed)
                return;

            handle.Dispose();
            disposed = true;
        }
    }
}