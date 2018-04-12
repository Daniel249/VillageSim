using System;
using System.Collections.Generic;

class Interface {
    int TitelLocation_x;

    static DataPanels savedData;
    static DataPanels currentData;

    // update currentData
    public static void updateCurrentData(TimeSpan[] current) {
        currentData.UpdatePrint(current);
    }
    // update savedData
    public static void storeCurrent() {
        savedData.UpdatePrint(currentData.logs);
    }

    // constructor
    public static void initInterface() {
        int profAmmount = Simulation.SimInstance.profAmmount;

        savedData = new DataPanels(15, 5, profAmmount);
        currentData = new DataPanels(15, 15, profAmmount);


    }
}