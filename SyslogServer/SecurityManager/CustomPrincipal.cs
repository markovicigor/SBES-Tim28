using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public enum Roles
    {
        Readers, Operators, Admins
    }
    public enum Permissions
    {
        Read, Update, Delete
    }
    public class CustomPrincipal : IPrincipal
    {
        WindowsIdentity windowsIdentity;
        List<Roles> uloge = new List<Roles>();
        List<Permissions> permisije = new List<Permissions>();

        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            this.WindowsIdentity = windowsIdentity;


            foreach(IdentityReference groupId in this.WindowsIdentity.Groups)
            {
                string group = groupId.Translate(typeof(NTAccount)).ToString();
                string[] split = group.Split('\\');


                if (split[split.Length - 1] == "Readers")
                {
                    this.uloge.Add(Roles.Readers);
                    this.permisije.Add(Permissions.Read);
                }
                else if (split[split.Length - 1] == "Operators")
                {
                    this.uloge.Add(Roles.Operators);
                    this.permisije.Add(Permissions.Update);
                }
                else if (split[split.Length - 1] == "Admins")
                {
                    this.uloge.Add(Roles.Admins);
                    this.permisije.Add(Permissions.Delete);
                }
            }

        }

        public IIdentity Identity => throw new NotImplementedException();

        public List<Roles> Uloge { get => uloge; set => uloge = value; }
        public List<Permissions> Permisije { get => permisije; set => permisije = value; }
        public WindowsIdentity WindowsIdentity { get => windowsIdentity; set => windowsIdentity = value; }

        public bool IsInRole(string permisija)
        {
            Enum.TryParse(permisija, out Permissions vrstaPermisije);

            if (this.Permisije.Contains(vrstaPermisije))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
