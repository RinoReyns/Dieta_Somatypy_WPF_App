namespace Dieta_somatypy_WPF.Repo
{
    public class Wzory
    {
        private double M_10_18(double masa)
        {
            return ((masa * 17.5) + 651);
        }
        private double K_10_18(double masa)
        {
            return ((masa * 12.2) + 746);
        }
        private double M_19_30(double masa)
        {
            return ((masa * 15.3)+679);
        }
        private double K_19_30(double masa)
        {
            return ((masa * 14.7) + 479);
        }
        private double M_31_60(double masa)
        {
            return ((masa * 11.6) + 879);
        }
        private double K_31_60(double masa)
        {
            return ((masa * 9.7) + 829);
        }
        private double M_61(double masa)
        {
            return ((masa * 13.5) + 487);
        }
        private double K_61(double masa)
        {
            return ((masa * 10.5) + 596);
        }
        public double BMR_aktywnosc(double BMR, double aktywnosc)
        {
            return (BMR * aktywnosc);
        }
        public double Man(int wiek, double masa)
        {
            if (wiek <= 18)
            {
                return M_10_18(masa);
            }
            else if (18 < wiek && wiek <= 30)
            {
                return M_19_30(masa);
            }
            else if (30 < wiek && wiek <= 60)
            {
                return M_31_60(masa);
            }
            else if (60 < wiek)
            {
                return M_61(masa);
            }
            else
                return 0;

        }

        public double Woman(int wiek, double masa)
        {
            if (wiek <= 18)
            {
                return  K_10_18(masa);
            }
            else if (18 < wiek && wiek <= 30)
            {
                return  K_19_30(masa);
            }
            else if (30 < wiek && wiek <= 60)
            {
                return  K_31_60(masa);
            }
            else if (60 < wiek)
            {
                return  K_61(masa);
            }
            else
                return 0;
        }
    }
}
