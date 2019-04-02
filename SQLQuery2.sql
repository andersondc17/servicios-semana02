use negocios2019
go

create proc sp_pedidos
as
select IdPedido,FechaPedido,NombreCia,
DireccionDestinatario,CiudadDestinatario
from tb_pedidoscabe p join tb_clientes c
on p.IdCliente=c.IdCliente
go

create proc sp_pedidos_year
@y int
as
select IdPedido,FechaPedido,NombreCia,
DireccionDestinatario,CiudadDestinatario
from tb_pedidoscabe p join tb_clientes c
on p.IdCliente=c.IdCliente
where Year(FechaPedido)=@y
go

exec sp_pedidos_year 2019
go