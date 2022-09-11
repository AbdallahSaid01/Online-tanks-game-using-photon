# Online-tanks-game-using-photon
This is an online tank shooter prototype made using unity and photon<br/><br/>
So far the project includes:<br/>
- A small map with distructable walls and power-ups that reduce the timer between each bullet<br/>
- 2 to 4 player tanks controlled by individual instances of the game<br/>
- A log-in screen where you can log in with a username and create/join rooms<br/>
- A max of 2 AI controlled tanks implemented using a 2 state FSM and state design pattern that alternate between chasing and roaming<br/>
the AI tanks replace the missing player tanks<br/><br/>

The things I am planning to implement are:<br/>
- A win/loss condition and screens
- A health system with the health bar displayed above each individual tank except for the AI tanks since they are indistructable
- Improving the AI of the AI tanks to be more deterministic and act based on the other players health and aquisition of power-ups
