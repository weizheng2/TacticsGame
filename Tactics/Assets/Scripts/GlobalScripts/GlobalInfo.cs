using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
	public static GlobalInfo m_instance;
	public EnemyDB enemyDB;

	public static GlobalInfo GetInstance()
	{
		if (m_instance == null)
			m_instance = new GlobalInfo();

		return m_instance;
	}

	public void Awake()
	{
		if (!m_instance)
		{
			m_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}

}
