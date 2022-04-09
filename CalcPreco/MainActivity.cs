using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Graphics;

namespace CalcPreco
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText _volume1, _volume2, _volume3, _volume4;
        EditText _preco1, _preco2, _preco3, _preco4;
        Button _btnCalcular, _btnLimpar, _btnDaltonico;
        TextView _txtValor1, _txtValor2, _txtValor3, _txtValor4;
        int posicao = 4;
        decimal menor = 1000;
        bool ModoDaltonico = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            criaRefInput();
            _btnLimpar = FindViewById<Button>(Resource.Id.btnLimpar);
            _btnLimpar.Click += _btnLimpar_Click;
            
            _btnCalcular = FindViewById<Button>(Resource.Id.btnCalcular);
            _btnCalcular.Click += _btnCalcular_Click;

            _btnDaltonico = FindViewById<Button>(Resource.Id.btnDaltonico);

            _btnDaltonico.Click += _btnDaltonico_Click;
        }
        private void _btnDaltonico_Click(object sender, System.EventArgs e)
        {
            EditText[] campos = getCampos();
            TextView[] camposTextView = getTextView();
            if (ModoDaltonico)
            {
                ModoDaltonico = false;
                for (int i = 0; i < campos.Length; i++)
                {
                    campos[i].SetBackgroundResource(Resource.Drawable.bordaRedondaAmarelo);
                    campos[i].SetTextColor(Color.Black);
                }
                for (int i = 0; i < camposTextView.Length; i++)
                {
                    camposTextView[i].SetBackgroundResource(Resource.Drawable.bordaRedondaGold);
                    if(posicao != 4)
                    {
                        camposTextView[posicao].SetTextColor(Color.Black);
                        camposTextView[posicao].SetBackgroundResource(Resource.Drawable.btnCalcular);
                    } 
                }
            }
            else
            {
                ModoDaltonico = true;
                for (int i = 0; i < campos.Length; i++)
                {
                    campos[i].SetBackgroundResource(Resource.Drawable.bordaRedondaRoxo);
                    campos[i].SetTextColor(Color.DarkCyan);
                }
                for (int i = 0; i < camposTextView.Length; i++)
                {
                    camposTextView[i].SetBackgroundResource(Resource.Drawable.bordaRedondaRosa);
                    if (posicao != 4)
                    {
                        camposTextView[posicao].SetTextColor(Color.DarkCyan);
                        camposTextView[posicao].SetBackgroundResource(Resource.Drawable.bordaRedondaRoxo);
                    }
                }
            }
        }
        public EditText[] getPreco()
        {
            EditText[] preco =
            {
                _preco1,
                _preco2,
                _preco3,
                _preco4
            };
            return preco;
        }
        public EditText[] getVolume()
        {
            EditText[] volume =
            {
                _volume1,
                _volume2,
                _volume3,
                _volume4
            };
            return volume;
        }
        public void validFieldsEmpty()
        {
            EditText[] preco = getPreco();
            EditText[] vol = getVolume();
            /*if (_preco1.Text == "" || _volume1.Text == "")
            {
                _preco1.Text = "15000";
                _volume1.Text = "5000";
            }
            if (_preco2.Text == "" || _volume2.Text == "")
            {
                _preco2.Text = "15000";
                _volume2.Text = "5000";
            }
            if (_preco3.Text == "" || _volume3.Text == "")
            {
                _preco3.Text = "15000";
                _volume3.Text = "5000";
            }
            if (_preco4.Text == "" || _volume4.Text == "")
            {
                _preco4.Text = "15000";
                _volume4.Text = "5000";
            }*/

            // Se tiver campos vazios seta valores em no vol e no preco na posição do index
            for(int i = 0; i < preco.Length; i++)
            {
                if (preco[i].Text == "")
                {
                    preco[i].Text = "15000";
                    vol[i].Text = "5000";
                }
            }
            for (int i = 0; i < vol.Length; i++)
            {
                if (vol[i].Text == "")
                {
                    vol[i].Text = "5000";
                    preco[i].Text = "15000";
                }
            }
        }
        public void setDefaultStyle()
        {
            TextView[] camposTextView = getTextView();

            for (int i = 0; i < camposTextView.Length; i++)
            {
                if (ModoDaltonico)
                {
                    camposTextView[i].SetBackgroundResource(Resource.Drawable.bordaRedondaRosa);
                }
                else
                {
                    camposTextView[i].SetBackgroundResource(Resource.Drawable.bordaRedondaGold);
                }    
            }      
        }
        public void showBestOption()
        {
            TextView[] camposTextView = getTextView();
            EditText[] volume = getVolume();
            EditText[] preco = getPreco();
            validFieldsEmpty();
            setDefaultStyle();

            // pega o menor valor
            for (int i = 0; i < camposTextView.Length; i++)
            {
                if (decimal.Parse(camposTextView[i].Text) < menor)
                {
                    menor = decimal.Parse(camposTextView[i].Text);
                    posicao = i;
                }
                if (decimal.Parse(camposTextView[i].Text) >= 3000)
                {
                    camposTextView[i].Text = "";
                }
            }
            // reseta todos os campos que não forem preenchidos
            for (int i = 0; i < volume.Length; i++)
            {
                if (int.Parse(volume[i].Text) == 5000)
                {
                    volume[i].Text = "";
                }
            }
            for (int i = 0; i < preco.Length; i++)
            {
                if (decimal.Parse(preco[i].Text) == 15000)
                {
                    preco[i].Text = "";
                }
            }
            // só seta o menor valor se ele for chamado
            for (int i = 0; i < camposTextView.Length; i++)
            {        
                if (menor != 1000)
                {

                    if (ModoDaltonico)
                    {
                        setDefaultStyle();
                        if (posicao != 4)
                        {
                            camposTextView[posicao].SetBackgroundResource(Resource.Drawable.bordaRedondaRoxo);
                            camposTextView[posicao].SetTextColor(Color.DarkCyan);
                        }
                    }
                    else
                    {
                        setDefaultStyle();
                        if (posicao != 4)
                        {
                            camposTextView[posicao].SetBackgroundResource(Resource.Drawable.btnCalcular);
                            camposTextView[posicao].SetTextColor(Color.Black);
                        }
                    }                
                }
            }       
        }
        private void _btnCalcular_Click(object sender, System.EventArgs e)
        {
            validFieldsEmpty();
            calculaPreco();
            showBestOption();
        }
        public void calculaPreco()
        {
            validFieldsEmpty();

            decimal preco1 = decimal.Parse(_preco1.Text);
            int volume1 = int.Parse(_volume1.Text);
            decimal valorLitro1 = (preco1 / volume1) * 1000;

            decimal preco2 = decimal.Parse(_preco2.Text);
            int volume2 = int.Parse(_volume2.Text);
            decimal valorLitro2 = (preco2 / volume2) * 1000;

            decimal preco3 = decimal.Parse(_preco3.Text);
            int volume3 = int.Parse(_volume3.Text);
            decimal valorLitro3 = (preco3 / volume3) * 1000;

            decimal preco4 = decimal.Parse(_preco4.Text);
            int volume4 = int.Parse(_volume4.Text);
            decimal valorLitro4 = (preco4 / volume4) * 1000;

            _txtValor1.Text = valorLitro1.ToString("F2");
            _txtValor2.Text = valorLitro2.ToString("F2");
            _txtValor3.Text = valorLitro3.ToString("F2");
            _txtValor4.Text = valorLitro4.ToString("F2");
        }
        private void _btnLimpar_Click(object sender, System.EventArgs e)
        {
            CleanFields();
        }
        public void CleanFields()
        {
            // limpa todos os campos
            EditText[] campos = getCampos();
            TextView[] camposTextView = getTextView();
            for (int i = 0; i < campos.Length; i++)
            {
                campos[i].Text = "";
            }
            for (int i = 0; i < camposTextView.Length; i++)
            {
                camposTextView[i].Text = "";
            }
            // reseta o estilo e a melhor opção
            setDefaultStyle();
            menor = 1000;
            posicao = 4;
        }
        public EditText[] getCampos()
        {
            EditText[] campos =
            {
                _volume1,
                _volume2,
                _volume3,
                _volume4,
                _preco1,
                _preco2, 
                _preco3,
                _preco4
            };
            return campos;
        }
        public TextView[] getTextView()
        {
            TextView[] camposView =
            {
                _txtValor1,
                _txtValor2,
                _txtValor3,
                _txtValor4
            };
            return camposView;
        }
        public void criaRefInput()
        {
            _volume1 = FindViewById<EditText>(Resource.Id.volume1);
            _volume2 = FindViewById<EditText>(Resource.Id.volume2);
            _volume3 = FindViewById<EditText>(Resource.Id.volume3);
            _volume4 = FindViewById<EditText>(Resource.Id.volume4);

            _preco1 = FindViewById<EditText>(Resource.Id.preco1);
            _preco2 = FindViewById<EditText>(Resource.Id.preco2);
            _preco3 = FindViewById<EditText>(Resource.Id.preco3);
            _preco4 = FindViewById<EditText>(Resource.Id.preco4);

            _txtValor1 = FindViewById<TextView>(Resource.Id.txtValor1);
            _txtValor2 = FindViewById<TextView>(Resource.Id.txtValor2);
            _txtValor3 = FindViewById<TextView>(Resource.Id.txtValor3);
            _txtValor4 = FindViewById<TextView>(Resource.Id.txtValor4);
        }
    }
}