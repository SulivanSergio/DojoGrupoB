

--TORRE = 0
--CENTRO = 1
--SEGUIR = 2
--PARADO = 3
a = 0

function Start()
    ChangeState(1)	
end

function Update(gameTime)
	if (ColidiuPlayer() == true) then
		ChangeState(1)
	end
	if (ConquistouTorre() == true) then
		ChangeState(1)
	end
	if (PegouArma() == true) then
		if(RetornaItem() ~= 2)then
			
			ChangeState(2)
			
		end
	end

end