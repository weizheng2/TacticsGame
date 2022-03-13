using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Assets/Scripts/ScriptableObjects/Enemy", order = 1)]
public class EnemyDB : ScriptableObject
{
	public EnemyData[] enemies;

	Dictionary<int, int> enemiesById;

	public void Initialize()
	{
		enemiesById = new Dictionary<int, int>();

		for (int i = 0; i < enemies.Length; i++)
		{
			enemiesById.Add(enemies[i].id, i);
		}
	}

	public EnemyData GetEnemy(int id)
	{
		if (enemiesById.ContainsKey(id))
		{
			return enemies[enemiesById[id]];
		}
		return null;
	}


	[ContextMenu("Fill Data")]
	public void FillData()
	{
		// Json to string
		string jsonPath = "Assets/Scripts/ScriptableObjects/DB_Json/enemy_data.json";
		string jsonString = File.ReadAllText(jsonPath);

		// String to items
		enemies = JsonConvert.DeserializeObject<EnemyData[]>(jsonString);
	}

}

[Serializable]
public class EnemyData
{
	public int id;
	public string name;
	public int health_points;
	public int damage;
	public int speed;
	public int experience_given;
	public int gold_given;
}