using System;
namespace laboratorio7
{
    public class Fila
    {
        public int regionID;
        public string region;
        public int comunaID;
        public string comuna;
        public int administrationID;
        public string administration;
        public double psu;
        public double nem;
        public int numeroDatos;

        public Fila(string filaCsv)
        {
            string[] partes = filaCsv.Split(',');
            regionID = Convert.ToInt32(partes[0]);
            region = partes[1];
            comunaID = Convert.ToInt32(partes[2]);
            comuna = partes[3];
            administrationID = Convert.ToInt32(partes[4]);
            administration = partes[5];
            // Los siguientes datos pueden ser vacíos, por eso la verificación
            if (partes[6] != "") psu = Convert.ToDouble(partes[6]);
            if (partes[7] != "") nem = Convert.ToDouble(partes[7]);
            numeroDatos = (int) Convert.ToDouble(partes[8]);
        }

        public override string ToString()
        {
            return $"{administration} | PSU : {psu} | NEM {nem}";
        }
    }
}
