using System;
using System.Collections.Generic;
using gasstation.code.core.logging;

namespace gasstation.code.core.attributes
{
    // Attach to an object
    [Serializable]
    public class Attributes
    {
        public string friendlyAttributeName;
        public List<Attribute> attributes;
        public Dictionary<string, Attribute> quickLookupAttributes = new Dictionary<string, Attribute>() { };


        public Attributes(List<Attribute> atts, string name = "none")
        {
            this.attributes = atts;
            this.friendlyAttributeName = name;

            this.InitializeQuickLookup();
            this.PrintAttributes();
        }

        private void InitializeQuickLookup()
        {
            if (this.attributes != null)
            {
                foreach (Attribute att in this.attributes)
                {
                    string attstring = att.GetType().Name;
                    LogFactory.DEBUG($"Adding attribute {attstring}");
                    this.quickLookupAttributes[att.GetType().Name] = att;
                }
            }
        }

        public void PrintAttributes()
        {
            if (this.attributes != null)
            {
                LogFactory.DEBUG($"Attributes configured: {this.friendlyAttributeName}");
                foreach (Attribute att in this.attributes)
                {
                    LogFactory.DEBUG($"Attribute: {att.GetType().Name}");
                }
            }
        }

        public Attribute GetAttribute(string name)
        {
            if (this.quickLookupAttributes.ContainsKey(name))
            {
                return this.quickLookupAttributes[name];
            } else {
                return null;
            }
        }

        public bool HasAttribute(string name)
        {
            return this.GetAttribute(name) != null;
        }

        public Health GetHealth()
        {
            if (this.HasAttribute("Health"))
            {
                return (Health)this.quickLookupAttributes["Health"];
            } else
            {
                return null;
            }
        }

        public Trust GetTrust()
        {
            if (this.HasAttribute("Trust"))
            {
                return (Trust)this.quickLookupAttributes["Trust"];
            }
            else
            {
                return null;
            }
        }

        public StreetCred GetStreetCred()
        {
            if (this.HasAttribute("StreetCred"))
            {
                return (StreetCred)this.quickLookupAttributes["StreetCred"];
            }
            else
            {
                return null;
            }
        }

        public void Heal(int healing)
        {
            Health h = this.GetHealth();
            if(h != null)
            {
                h.increment(healing);
                this.quickLookupAttributes["Health"] = h;
            }
        }

        public void Damage(int dmg)
        {
            Health h = this.GetHealth();
            if (h != null)
            {
                h.decrement(dmg);
                this.quickLookupAttributes["Health"] = h;
            }
        }


        public void Trust(int uTrust)
        {
            Trust t = this.GetTrust();
            if(t != null)
            {
                t.increment(uTrust);
                this.quickLookupAttributes["Trust"] = t;
            }
        }

        public void UnTrust(int uTrust)
        {
            Trust t = this.GetTrust();
            if (t != null)
            {
                t.decrement(uTrust);
                this.quickLookupAttributes["Trust"] = t;
            }
        }
    }

    // Base of attributes
    [Serializable]
    public class Attribute
    {
        public int value;
        public Attribute(int value)
        {
            this.value = value;
        }

        public virtual void decrement()
        {
            if (this.value > 0)
            {
                this.value -= 1;
            }
        }

        public virtual int getValue()
        {
            return this.value;
        }

        public virtual void increment()
        {
            this.value += 1;
        }

        public virtual void increment(int incValue)
        {
            this.value += incValue;
        }

        public virtual void decrement(int decValue)
        {
            this.value -= decValue;
        }
    }

    [Serializable]
    public class Trust : Attribute
    {

        public Trust(int trust) : base(trust)
        {
        }
    }
    [Serializable]
    public class StreetCred : Attribute
    {

        public StreetCred(int cred) : base(cred)
        {
        }
    }
    [Serializable]
    public class Health: Attribute
    {
        public Health(int health) : base(health)
        {
        }

        public bool IsDead()
        {
            return this.value > 0 == false;
        }
    }
}
