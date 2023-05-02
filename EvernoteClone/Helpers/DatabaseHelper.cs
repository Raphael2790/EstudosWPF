using EvernoteClone.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EvernoteClone.Helpers;

public static class DatabaseHelper
{
    private static readonly string dbFile = Path.Combine(Environment.CurrentDirectory,"notesDb.db3");
    private static readonly string dbPath = "https://sua-app.firebaseio.com/";

    public static bool Insert<T>(T item)
    {
        using var connection = new SQLiteConnection(dbFile);
        connection.CreateTable<T>();
        return connection.Insert(item) > 0;
    }

    public static async Task<bool> InsertCloud<T>(T item)
    {
        string json = JsonSerializer.Serialize(item);

        using HttpClient httpClient = new();

        HttpResponseMessage response = await httpClient
            .PostAsync($"{dbPath}{typeof(T).Name}.json", new StringContent(json, Encoding.UTF8));

        return response.IsSuccessStatusCode;
    }

    public static bool Update<T>(T item)
    {
        using var connection = new SQLiteConnection(dbFile);
        connection.CreateTable<T>();
        return connection.Update(item) > 0;
    }

    public static async Task<bool> UpdateCloud<T>(T item) where T : IHasId
    {
        string json = JsonSerializer.Serialize(item);
        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient
            .PatchAsync($"{dbPath}{typeof(T).Name}/{item.Id}.json", new StringContent(json, Encoding.UTF8));
        return response.IsSuccessStatusCode;
    }

    public static bool Delete<T>(T item)
    {
        using var connection = new SQLiteConnection(dbFile);
        connection.CreateTable<T>();
        return connection.Delete(item) > 0;
    }

    public static async Task<bool> DeleteCloud<T>(T item) where T : IHasId
    {
        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient
            .DeleteAsync($"{dbPath}{typeof(T).Name}/{item.Id}.json");
        return response.IsSuccessStatusCode;
    }

    public static List<T> Read<T>() where T : new()
    {
        using var connection = new SQLiteConnection(dbFile);
        connection.CreateTable<T>();
        return connection.Table<T>().ToList();
    }

    public static async Task<List<T>> ReadCloud<T>() where T : IHasId
    {
        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.GetAsync($"{dbPath}{typeof(T).Name}.json");
        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Dictionary<string, T>>(json);

            var list = result?.Select(x =>
            {
                var item = x.Value;
                x.Value.Id = x.Key;
                return item;
            }).ToList();

            return list!;
        }
        else
        {
            return Enumerable.Empty<T>().ToList();
        }
    }
}
