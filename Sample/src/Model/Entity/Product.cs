using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;

namespace Model.Entity
{
    public partial class Product:IEntity,INotifyDataErrorInfo
    {
        public int ID { get; set; }

        private string mvString = string.Empty;
        public string Number { get { return mvString; } 
            set 
            { 
                mvString = value;
                bool valid = true;
                foreach (char c in mvString)
                {
                    if (!Char.IsLetterOrDigit(c))
                    {
                        valid = false;
                        //break;
                    }
                    if (!valid)
                    {
                        List<string> errors = new List<string>();
                        errors.Add("The ModelNumber can only contain letters and numbers.");
                        SetErrors("ModelNumber", errors);
                        break;
                    }
                    else
                    {
                        ClearErrors("ModelNumber");
                    }
            } 
            }
        }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
        public int idCategory { get; set; }
        public bool IsExpensive { get { return this.Cost > 100; } }

        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (errors.Values);
            }
            else
            {
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }
        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            errors.Remove(propertyName);
            errors.Add(propertyName, propertyErrors);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
        private void ClearErrors(string propertyName)
        {
            errors.Remove(propertyName);
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
