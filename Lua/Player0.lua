

--TORRE = 0
--CENTRO = 1
--SEGUIR = 2
--PARADO = 3


function Start()
    ChangeState(1)	
end

function Update(gameTime)
	if (ColidiuPlayer() == true) then
		ChangeState(1)
        
	end
	if (ConquistouTorre() == true) then
		if(RetornaItem ~= 0)then
			ChangeState(1)
        	end
	end
	if (PegouArma() == true) then
		ChangeState(2)
	end

end