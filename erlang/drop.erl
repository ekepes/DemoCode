-module(drop). 
-export([fall_velocity/1]).
<<<<<<< HEAD

=======
>>>>>>> 637c06a3f3080246097502a4c491cd8c574983f3
fall_velocity({Planemo, Distance}) -> fall_velocity(Planemo, Distance).

fall_velocity(Planemo, Distance) when Distance >= 0 ->
	Gravity = case Planemo of
		earth -> 9.8;
		moon -> 1.6;
		mars -> 3.71
	end, % note comma - function isn't done yet
	math:sqrt(2 * Gravity * Distance).
