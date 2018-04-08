using System;

abstract class Person {

    // profession
    // main product location in inventory
    public abstract Profession Role { get; }

    // Inventory
    public int[] Inventory { get; private set; }

    // Currency
    public decimal Cash { get; private set; }

    // close deal
    public void Transaction(decimal cash, int resourceID, int resourceAmmount) {
        Inventory[resourceID] += resourceAmmount;
        Cash += cash;
    }


    // methods

    // main method
    public void turn() {
        work();
        considerSell();
        considerBuy();
    }

    // override by inherited classes
    public abstract void work();
    

    // sell logic
    void considerSell() {
        // check inventory and role. think and then sell
    }

    // buy logic
    protected virtual void considerBuy() {

    }

    // set inventory. on respawn only
    public void setInventory(int[] _inventory) {
        Inventory = _inventory;
    }

    // constructor
    protected Person() {
        Inventory = new int[Simulation.SimInstance.profAmmount];
    }
}