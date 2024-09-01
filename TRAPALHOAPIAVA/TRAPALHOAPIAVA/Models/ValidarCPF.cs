namespace TRAPALHOAPIAVA.Models
{
    public static class ValidarCPF
    {
        public static bool ValidaCPF(string cpf)
        {
            cpf = cpf.Replace(".", "");
            cpf = cpf.Replace("-", "");

            if (cpf.Length > 11 || cpf.Length < 11)
            {
                return false;
            }

            int[] produto = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int multi = 0;

            for (int i = 0; i < cpf.Length - 2; i++)
            {
                multi += Convert.ToInt32(cpf[i].ToString()) * produto[i];
            }

            int dig1 = 0;
            if (multi % 11 < 2)
            {
                dig1 = 0;
            }

            else if (multi % 11 >= 2)
            {
                dig1 = 11 - (multi % 11);
            }


            int[] produto2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int multi2 = 0;

            for (int i = 0; i < cpf.Length - 1; i++)
            {
                multi2 += Convert.ToInt32(cpf[i].ToString()) * produto2[i];
            }

            int dig2 = 0;

            if (multi2 % 11 < 2)
            {
                dig2 = 0;
            }

            else if (multi2 % 11 >= 2)
            {
                dig2 = 11 - (multi2 % 11);
            }

            if (Convert.ToInt32(cpf[9].ToString()) == dig1 && Convert.ToInt32(cpf[10].ToString()) == dig2)
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

//validação de 2022 - Com Prof. Danilo
