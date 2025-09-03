using Microsoft.Maui.Storage;
using ModuERP.Core.Interfaces;

namespace ModuERP
{
    public class MauiSessionStorage : ISessionStorage
    {
        public void Set(string key, string value) => Preferences.Set(key, value);
        public string? Get(string key) => Preferences.Get(key, null);
        public void Remove(string key) => Preferences.Remove(key);
    }
}
