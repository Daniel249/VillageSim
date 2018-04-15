using System;
using System.Collections.Generic;

class Interface {

    static DataPanels savedData;
    static DataPanels currentData;
    static Panel currentDems;

    // update currentData
    public static void updateCurrentData(TimeSpan[] current) {
        currentData.UpdatePrint(current);
        // print current program size in ram
        Console.SetCursorPosition(0,0);
        Console.WriteLine(GC.GetTotalMemory(true));
    }
    // update savedData
    public static void storeCurrentData() {
        savedData.UpdatePrint(currentData.logs);
    }

    public static void printDems() {
        VillageSim instance = (VillageSim)Simulation.SimInstance;
        string dems = "";
        for(int i = 0; i < instance.profAmmount; i++) {
            dems += instance.Demographics[i].Count + "     ";
        }
        currentDems.PrintText(dems, 0);
        currentDems.PrintText(instance.DeadPeople.Count.ToString() + "    ", 1);
    }

    // constructor
    public static void initInterface() {
        Console.Clear();
        int profAmmount = ((VillageSim)(Simulation.SimInstance)).profAmmount;
        
        savedData = new DataPanels(15, 5, profAmmount);
        currentData = new DataPanels(15, 15, profAmmount);

        currentDems = new Panel(15, 2);
    }
}