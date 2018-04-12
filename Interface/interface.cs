using System;
using System.Collections.Generic;

class Interface {

    static DataPanels savedData;
    static DataPanels currentData;

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

    // constructor
    public static void initInterface() {
        Console.Clear();
        int profAmmount = ((VillageSim)(Simulation.SimInstance)).profAmmount;
        
        savedData = new DataPanels(15, 5, profAmmount);
        currentData = new DataPanels(15, 15, profAmmount);


    }
}