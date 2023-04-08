

--TORRE = 0
--CENTRO = 1
--SEGUIR = 2
--PARADO = 3


function Start()
    ChangeState(0)	
end

function Update(gameTime)
	if (ColidiuPlayer() == true) then
		if(RetornaItem() ~= 0) then
			ChangeState(0)
        end
	end
	if (ConquistouTorre() == true) then
		if(RetornaItem() ~= 0) then
			ChangeState(0)
        end
	end
	if (PegouArma() == true) then
		ChangeState(0)
	end

end