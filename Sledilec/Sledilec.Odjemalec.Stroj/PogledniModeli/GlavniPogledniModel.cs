using Newtonsoft.Json;
using Sledilec.Odjemalec.Stroj.Modeli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sledilec.Odjemalec.Stroj.PogledniModeli
{
    public class GlavniPogledniModel : INotifyPropertyChanged
    {
        private HttpClient povezava;
        public event PropertyChangedEventHandler PropertyChanged;
        public IList<NadzorovaniModel> Seznam { get; private set; }
        public string NovoIme { get; set; }
        public string NoviPriimek { get; set; }
        public RelayCommand DodajUkaz { get; }
        public string Napaka { get; private set; }
        public bool Nalaganje { get; private set; }
        public GlavniPogledniModel()
        {
            povezava = new HttpClient { BaseAddress = Nastavitve.Strežnik };
            var ignore = NaložiVseAsync(CancellationToken.None);
            DodajUkaz = new RelayCommand(Dodaj, () => !string.IsNullOrEmpty(NovoIme) && !string.IsNullOrEmpty(NoviPriimek));
        }
        public string Strežnik
        {
            get { return Nastavitve.Strežnik.ToString(); }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            switch (name)
            {
                case nameof(NovoIme):
                case nameof(NoviPriimek):
                    DodajUkaz.RaiseCanExecuteChanged();
                    break;
            }
        }

        public async Task NaložiVseAsync(CancellationToken ct)
        {
            Nalaganje = true;
            try
            {
                var result = await povezava.GetStringAsync("Nadzorovani/SeznamVseh");
                Seznam = JsonConvert.DeserializeObject<List<NadzorovaniModel>>(result);
            }
            catch (Exception ex)
            {
                Napaka = ex.Message;
            }
            finally
            {
                Nalaganje = false;
            }
        }

        public async void Dodaj()
        {
            try
            {
                NadzorovaniModel model = new NadzorovaniModel { Ime = NovoIme, Priimek = NoviPriimek };
                var vsebina = new StringContent(JsonConvert.SerializeObject(model), encoding: Encoding.UTF8, mediaType: "application/json");
                await povezava.PostAsync("Nadzorovani/Dodaj", vsebina);
                NovoIme = null;
                NoviPriimek = null;
                var ignore = NaložiVseAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                Napaka = ex.Message;
            }
        }
    }
}
