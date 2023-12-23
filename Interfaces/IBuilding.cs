
public interface IBuilding
{
    void UpgradeBuilding();
    void RepairBuilding();
    void TryUpgrade(int currentMoney);
    void TryRepair(int currentMoney);

}
