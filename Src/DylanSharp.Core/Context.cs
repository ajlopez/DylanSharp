namespace DylanSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Context
    {
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private IDictionary<string, string> types = new Dictionary<string, string>();
        private Context parent;

        public Context()
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }

        public bool HasValue(string name)
        {
            if (this.values.ContainsKey(name))
                return true;

            if (this.parent != null)
                return this.parent.HasValue(name);

            return false;
        }

        public bool HasLocalValue(string name)
        {
            return this.values.ContainsKey(name);
        }

        public string GetType(string name)
        {
            if (this.values.ContainsKey(name))
                if (this.types.ContainsKey(name))
                    return this.types[name];
                else
                    return null;

            if (this.parent != null)
                return this.parent.GetType(name);

            return null;
        }

        public void SetType(string name, string type)
        {
            this.types[name] = type;

            if (!this.values.ContainsKey(name))
                this.values[name] = null;
        }
    }
}

