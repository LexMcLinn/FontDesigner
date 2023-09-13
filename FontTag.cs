using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFCreator
{
    [Serializable]
    public class FontTag
    {
        public byte[] byteTag;
        
        public FontTag() { this.byteTag = new byte[4]; }
        public FontTag(byte[] byteTag)
        {
            this.byteTag = byteTag;
        }

        public FontTag(string S)
        {
            this.byteTag = new byte[4];
            for (int i = 0; i < 4; i++) this.byteTag[i] = ((byte)S.ToCharArray()[i]);
        }


        public string TagName()
        {
            string TagS = "";
            for(int i =0; i<4; i++)
            {
                char c = Convert.ToChar(byteTag[i]);
                TagS += c;
            }
            
            return TagS;
        }
    }
}
