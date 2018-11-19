﻿using System.Collections.Generic;
using UnityEngine;


//List of quests stored on a player, holding references to all the quests they have been given
public class QuestLog : MonoBehaviour
{
    public static QuestLog inst;
    public List<Quest> quests;

    private void Awake()
    {
        inst = this;
    }
}