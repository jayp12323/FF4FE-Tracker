memory.usememorydomain("WRAM")

Shops={[8]=1,[9]=1,[10]=1,[12]=1,[16]=1,[24]=1,[32]=1,[69]=1,[70]=1,[75]=1,[76]=1,[203]=1,[204]=1,[225]=1,[226]=1,[227]=1,[228]=1,[229]=1,[230]=1,[231]=1,[233]=1,[234]=1,[235]=1,[236]=1,[258]=1,[261]=1,[270]=1,[273]=1,[306]=1,[321]=1,[322]=1,[323]=1,[357]=1}


store_items = {}    -- new array
for i=0, 7 do
  store_items[i] = 0
end

local function int2hex(loc)

	return string.upper(string.format("%02x",loc))

end

local function concat_array(array,len)

	string=""
	for i=0, len do
	  string = string .. "," .. array[i]
	end


	return string:sub(2)

end

local function printitems()

	if (memory.readbyte(0x1B55)==0) then
		return
	end

	current_items = {}

	for i=0,7 do
		current_items[i]=int2hex(memory.readbyte(0x1B55+0x04*i))
	end
	if store_items[0] ~= current_items[0]
	then
		current_location = memory.read_u16_be(0x1701)
		if Shops[current_location] then
			item_count = memory.readbyte(0x1B7D)
			for i=item_count,7 do
				current_items[i]=0
			end
			tcp=socket.tcp()
			if(tcp:connect('127.0.0.1',5555)) then
				print("Succesfully connected")
			else
				print("Connection failed, please start the AutoTracking Service and try again")
			end

			result = tcp:send(current_location ..  "," .. concat_array( current_items,7))
			tcp:close()
		end
	store_items=current_items

	end
end

local function	myframe()
	if emu.framecount()%180==0 then
		printitems()
	end
end




local socket = require("socket.core")




while true do
	myframe()
	emu.frameadvance()
end
