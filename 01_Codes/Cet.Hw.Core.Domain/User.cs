using Cet.Core;
using Cet.Hw.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cet.Hw.Core.Domain
{
    public class User : HwEntity
    {
        protected User() { }

        public User(bool iIsActive = true)
        {
            this.Id = Guid.NewGuid();
            this.IsActive = iIsActive;
        }

        #region Properties
        private Guid id;
        public Guid Id
        {
            get { return id; }
            private set { id = value; }
        }

        private string login;
        public string Login
        {
            get { return login; }
            private set { login = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            private set { password = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            private set { email = value; }
        }

        private bool isGroup;
        public bool IsGroup
        {
            get { return isGroup; }
            private set { isGroup = value; }
        }

        private bool? isActive;
        public bool? IsActive
        {
            get { return isActive; }
            private set { isActive = value; }
        }

        private Guid? userUID;
        public Guid? UserUID
        {
            get { return userUID; }
            private set { userUID = value; }
        }

        private string realm;
        public string Realm
        {
            get { return realm; }
            private set { realm = value; }
        }

        /// <summary>
        /// Record mark point to parent top level on kind of self
        /// </summary>
        private string path;
        public string Path
        {
            get { return path; }
            private set { path = value; }
        }

        private ICollection<User> childMembers;
        public virtual ICollection<User> ChildMembers
        {
            get { return childMembers; }
            private set { childMembers = value; }
        }
        #endregion

        public void SetLoginPassword(string iLogin, string iPassword)
        {
            this.Login = iLogin.Trim();
            UpdatePassword(iPassword);
        }

        public void UpdatePassword(string newPassword)
        {
            if (!string.IsNullOrWhiteSpace(newPassword.Trim()))
                this.password = MD5.Create().ComputeHash(newPassword);
        }

        public void SetPasswordNotEncript(string newPassword)
        {
            this.password = newPassword;
        }

        public void SetUserData(UserData request)
        {
            this.IsGroup = false;

            this.SetName(request.Name);
            this.SetEMail(request.Email);
            this.SetIsActive(request.IsActive);

            if (!string.IsNullOrWhiteSpace(request.Password))
                SetLoginPassword(request.Login, request.Password);

            if (UserUID.HasValue)
                this.UserUID = Guid.NewGuid();
        }

        public void SetIsActive(bool? iIsActive)
        {
            if (iIsActive.HasValue)
                this.IsActive = iIsActive.Value;
        }

        public void SetName(string iName)
        {
            this.Name = iName;
        }

        public void SetEMail(string iEMail)
        {
            this.Email = iEMail;
        }

        public void AddChildUser(User child)
        {
            ChildMembers.Add(child);
            //child.ParentMembers.Add(this);
            child.Path = !string.IsNullOrEmpty(this.Path)
                ? this.Path + child.Id.ToString() + "/"
                : this.Id.ToString() + "/" + child.Id.ToString() + "/";
        }

        public void RemoveChildUser(User child)
        {
            ChildMembers.Remove(child);
            //child.ParentMembers.Remove(this);
        }

        public void PrepareRemove()
        {
            var members = ChildMembers.ToList();
            foreach (var item in members)
            {
                this.ChildMembers.Remove(item);
            }

            //var parents = ParentMembers.ToList();
            //foreach (var item in parents)
            //{
            //    item.RemoveChildUser(this);
            //}
        }


    }
}
