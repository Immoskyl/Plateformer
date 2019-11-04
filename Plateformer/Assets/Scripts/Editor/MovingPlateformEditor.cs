using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MovingPlateform))]
public class MovingPlateformEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        MovingPlateform movingPlateform = (MovingPlateform)target;

        movingPlateform.moveType = (MovingPlateform.MoveType)EditorGUILayout.EnumPopup("mouvement type", movingPlateform.moveType);

        if (movingPlateform.moveType == MovingPlateform.MoveType.Linear)
        {
            movingPlateform.distanceL = EditorGUILayout.FloatField("Distance", movingPlateform.distanceL);

            EditorGUILayout.LabelField("Vitesses :");
            movingPlateform.xSpeedL = EditorGUILayout.FloatField("en x", movingPlateform.xSpeedL);
            //movingPlateform.ySpeedL = EditorGUILayout.FloatField("en y", movingPlateform.ySpeedL);
            
            EditorGUILayout.LabelField("Temps d'arrêt :");
            movingPlateform.rightTimeStopL = EditorGUILayout.FloatField("à droite", movingPlateform.rightTimeStopL);
            movingPlateform.leftTimeStopL = EditorGUILayout.FloatField("à gauche", movingPlateform.leftTimeStopL);
        }
        /*
        if (movingPlateform.moveType == MovingPlateform.MoveType.Rectangle)
        {
            movingPlateform.xDistanceFactor = EditorGUILayout.IntField("lolx", movingPlateform.xDistanceFactor);
            movingPlateform.yDistanceFactor = EditorGUILayout.IntField("loly", movingPlateform.yDistanceFactor);

            EditorGUILayout.LabelField("Distances :");
            movingPlateform.xDistanceR = EditorGUILayout.FloatField("en longueur", movingPlateform.xDistanceR);
            movingPlateform.yDistanceR = EditorGUILayout.FloatField("en largeur", movingPlateform.yDistanceR);

            EditorGUILayout.LabelField("Vitesses :");
            movingPlateform.xSpeedR = EditorGUILayout.FloatField("en x", movingPlateform.xSpeedR);
            movingPlateform.ySpeedR = EditorGUILayout.FloatField("en y", movingPlateform.ySpeedR);

            movingPlateform.horizontalFirst = EditorGUILayout.Toggle("Commence horizontalement", movingPlateform.horizontalFirst);

            EditorGUILayout.LabelField("Temps d'arrêt :");
            movingPlateform.upRightTimeStopR = EditorGUILayout.FloatField("en haut à droite", movingPlateform.upRightTimeStopR);
            movingPlateform.upLeftTimeStopR = EditorGUILayout.FloatField("en haut à gauche", movingPlateform.upLeftTimeStopR);
            movingPlateform.downLeftTimeStopR = EditorGUILayout.FloatField("en bas à gauche", movingPlateform.downLeftTimeStopR);
            movingPlateform.downRightTimeStopR = EditorGUILayout.FloatField("en bas à droite", movingPlateform.downRightTimeStopR);
        }*/
    }
    
}
