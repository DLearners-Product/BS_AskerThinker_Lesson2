using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class EnablerCmtsDB
{
    public string welcome;
    public string brain_gym_tree_pose;
    public string introduction;
    public string phonemic_awareness;
    public string visusl_activity;
    public string matching_pairs;
    public string brain_gym_ocean_breathing;
    public string fill_the_missing_letters;
    public string spin_wheel;
    public string passage_reading;
    public string flip_book;
    public string goodbye;

    public EnablerCmtsDB()
    {
        welcome = Main_Blended.OBJ_main_blended.enablerComments[0];
        brain_gym_tree_pose = Main_Blended.OBJ_main_blended.enablerComments[1];
        introduction = Main_Blended.OBJ_main_blended.enablerComments[2];
        phonemic_awareness = Main_Blended.OBJ_main_blended.enablerComments[3];
        visusl_activity = Main_Blended.OBJ_main_blended.enablerComments[4];
        matching_pairs = Main_Blended.OBJ_main_blended.enablerComments[5];
        brain_gym_ocean_breathing = Main_Blended.OBJ_main_blended.enablerComments[6];
        fill_the_missing_letters = Main_Blended.OBJ_main_blended.enablerComments[7];
        spin_wheel = Main_Blended.OBJ_main_blended.enablerComments[8];
        passage_reading = Main_Blended.OBJ_main_blended.enablerComments[9];
        flip_book = Main_Blended.OBJ_main_blended.enablerComments[10];
        goodbye = Main_Blended.OBJ_main_blended.enablerComments[11];
    }
}