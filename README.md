# HideStats
The HideStats plugin gives server owners full control over player stats and their visibility in the HUD. Configure which stats are infinite and whether they’re displayed or hidden, offering a tailored experience for your Unturned server.

What You Can Do:
Set Infinite Stats: Choose which stats (e.g., health, stamina, water) are infinite for players.

Hide Stats in HUD: Control whether specific stats are visible or hidden on the player HUD.

# Example Configuration:
```
<StatsManagerConfiguration xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <InfiniteStats>
    <Health>false</Health>
    <Food>false</Food>
    <Water>false</Water>
    <Virus>false</Virus>
    <Stamina>true</Stamina>
    <Oxygen>false</Oxygen>
  </InfiniteStats>
  <HideStats>
    <Health>false</Health>
    <Food>false</Food>
    <Water>false</Water>
    <Virus>false</Virus>
    <Stamina>true</Stamina>
    <Oxygen>false</Oxygen>
  </HideStats>
</StatsManagerConfiguration>
```
