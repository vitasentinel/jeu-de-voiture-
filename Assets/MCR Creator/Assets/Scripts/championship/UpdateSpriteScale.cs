using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSpriteScale : MonoBehaviour {

    public Image trackSprite;
    public Text trackName;
    public Text trackPositionInList;


    // Update sprite scale depending the size of the screen
    public void MCR_UpdateTrackSprite() {
        championshipM championshipManager = GameObject.Find("championshipManager").GetComponent<championshipM>();
        if(championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].scenesSprite[0]){
            trackSprite.sprite = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].scenesSprite[0];
            trackName.text = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].TracksName[0];
            trackPositionInList.text = "Track " + 
                1 + 
                "/" + 
                championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].TracksName.Count;

            //Difficulty 0: Easy 1: Medium 2 Difficult
            //int NewTrackDifficulty = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].AI_Difficulty[0];
            // Use a UI text to display the difficulty
        }



	}

    public void MCR_UpdateNextTrackSprite()
    {
        championshipM championshipManager = GameObject.Find("championshipManager").GetComponent<championshipM>();
        if (championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].scenesSprite[championshipManager.currentTrackInTheList]){
            trackSprite.sprite = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].scenesSprite[championshipManager.currentTrackInTheList];
            trackName.text = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].TracksName[championshipManager.currentTrackInTheList];
            trackPositionInList.text = "Track " +
                (championshipManager.currentTrackInTheList+1).ToString() +
                "/" +
                championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].TracksName.Count;

            //Difficulty 0: Easy 1: Medium 2 Difficult
            //int NewTrackDifficulty = championshipManager.champInventory.listOfChampionship[championshipManager.currentSelectedChampionship].AI_Difficulty[championshipManager.currentTrackInTheList];
            // Use a UI text to display the difficulty
        }
    }
}
