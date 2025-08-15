import React, { useState, useEffect, useMemo } from 'react';
import { Package, PlusCircle, MoreVertical, Edit, Trash2, Info } from 'lucide-react';
import api from '../../services/api';
import {toast} from "react-toastify";
import {
  PageContainer,
  PageHeader,
  TitleContainer,
  Title,
  PrimaryButton,
  Card,
  FilterContainer,
  Input,
  Select,
  Table,
  Th,
  Td,
  Tr,
  ProductImage,
  StockStatus,
  ActionButton,
} from './styles';
import { useNavigate } from 'react-router-dom';

const Reservas = () => {
  var navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('Todos');
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState([]); 

  useEffect(() => {
    const fetchReservas = async () =>{
      try{
        const response = await api.get('/reservas');
        console.log(response.data);
        setProducts(response.data);
      }catch(error){
        console.error("Failed to fetch products: ", error)
      }
      finally{
        setIsLoading(false);
      }
    };

    fetchReservas();
  }, []);

  const deleteReserva = async (id) =>{
    try{
      await api.delete('/reservas/' + id);
      toast.success("Reserva excluída com sucesso!")
    }catch{
      toast.error("Erro ao excluir a reserva.")
    }
  }

  const filteredReservas = useMemo(() => {
    return products.filter(product => {
      const searchMatch = product.name.toLowerCase().includes(searchTerm.toLowerCase());
      const statusMatch = statusFilter === 'Todos' || product.category === statusFilter;
      return searchMatch && statusMatch;
    });
  }, [products, searchTerm, statusFilter]);

  const formatCurrency = (value) => value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });


  if(isLoading){
    return (
    <PageContainer>
      <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
        Carregando Reservas...
      </div>
    </PageContainer>
  )
  }
  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Package size={32} color="#4f46e5" />
          <Title>Minhas Reservas</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/Products/Create")} >
          <PlusCircle size={20} />
          Adicionar Nova Reserva
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input 
            type="text" 
            placeholder="Buscar por nome..." 
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{flexGrow: 1}}
          />
          <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="Todos">Todas as Categorias</option>
            <option value="EmAndamento">Em Andamento</option>
            <option value="FuturasProximas">Futuras Próximas</option>
            <option value="FuturasNormais">Futuras Normais</option>
            <option value="Encerradas">Encerradas</option>
          </Select>
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th style={{width: '80px'}}></Th>
              <Th>Titulo</Th>
              <Th>Responsavel</Th>
              <Th>Data Inicío</Th>
              <Th>Data Fim</Th>
              <Th>Participantes Previstos</Th>
              <Th>Valor Hora</Th>
              <Th>Desconto</Th>
              <Th>Valor Total</Th>
            </tr>
          </thead>
          <tbody>
            {filteredReservas.map((reserva) => (
              <Tr key={reserva.id}>
                <Td style={{ fontWeight: 500 }}>{reserva.Titulo}</Td>
                <Td style={{ fontWeight: 500 }}>{reserva.Responsavel}</Td>
                <Td style={{ fontWeight: 500 }}>{reserva.DataInicio}</Td>
                <Td style={{ fontWeight: 500 }}>{reserva.DataFim}</Td>
                <Td style={{ fontWeight: 500 }}>{reserva.ParticipantesPrevistos}</Td>
                <Td>{formatCurrency(reserva.ValorHora)}</Td>
                <Td>{reserva.Desconto}%</Td>
                <Td>{formatCurrency(reserva.ValorTotal)}</Td>
                <Td>
                  <div style={{display: 'flex', justifyContent: 'flex-end', gap: '0.5rem'}}>
                    <ActionButton title="Informações sobre o Produto">
                      <Info size={18} />
                    </ActionButton>
                    <ActionButton title="Editar Produto">
                      <Edit size={18} />
                    </ActionButton>
                    <ActionButton title="Excluir Produto" onClick={() => deleteReserva(reserva.id)}>
                      <Trash2 size={18} />
                    </ActionButton>
                  </div>
                </Td>
              </Tr>
            ))}
          </tbody>
        </Table>
        {filteredReservas.length === 0 && (
          <div style={{ padding: '2rem', textAlign: 'center', color: '#6b7280' }}>
            Nenhuma reserva encontrada para os filtros selecionados.
          </div>
        )}
      </Card>
    </PageContainer>
  );
};

export default Reservas;
