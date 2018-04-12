using System;
using System.Collections.Generic;

class DataPanels {

    // panels
    List<Panel> panels;
    public TimeSpan[] logs { get; private set; }

    static int panelSize = 10;
    
    public void UpdatePrint(TimeSpan[] timespan) {
        logs = timespan;
        for(int i = 0; i < timespan.Length; i++) {
            panels[i].PrintText(((Profession)i).ToString(), (int)Stats.Class);
            panels[i].PrintText(timespan[i].Open.ToString("#.##"), (int)Stats.Open);
            panels[i].PrintText(timespan[i].Close.ToString("#.##"), (int)Stats.Close);
            panels[i].PrintText(timespan[i].High.ToString("#.##"), (int)Stats.High);
            panels[i].PrintText(timespan[i].Low.ToString("#.##"), (int)Stats.Low);
            panels[i].PrintText(timespan[i].ResourceVolume.ToString(), (int)Stats.ResourceVol);
            panels[i].PrintText(timespan[i].CurrencyVolume.ToString("#.##"), (int)Stats.CurrencyVol);
            decimal averagePrice;
            try {
                averagePrice = timespan[i].CurrencyVolume / timespan[i].ResourceVolume;
            } catch {
                averagePrice = 0m;
            }
            panels[i].PrintText(averagePrice.ToString("#.##"), (int)Stats.Close);
        }
    }

    
    // factory 
    public void InitPanels(int loc_x, int loc_y, int panelNum) {
        
        // int panelNum = Simulation.SimInstance.profAmmount;

        panels = new List<Panel>();
        for(int i = 0; i < panelNum; i++) {
            Panel datap = new Panel(loc_x, loc_y);
            panels.Add(datap);
            loc_x += panelSize;
        }
    }

    // constructor
    public DataPanels(int loc_x, int loc_y, int panelNum) {
        InitPanels(loc_x, loc_y, panelNum);
        logs = new TimeSpan[panelNum];
    }
}