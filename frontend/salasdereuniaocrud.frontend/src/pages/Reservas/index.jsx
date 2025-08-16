import React, { useState, useEffect, useMemo } from 'react';
import { Calendar, PlusCircle, Edit, Trash2, Info, Check, X, ChevronLeft, ChevronRight } from 'lucide-react';
import { useNavigate } from 'react-router-dom';
import { toast } from "react-toastify";
import api from '../../services/api'; // Using the real API service
import {
  PageContainer, PageHeader, TitleContainer, Title, PrimaryButton, Card,
  FilterContainer, Input, Select, Table, Th, Td, Tr, ActionButton,
  StatusBadge, EmptyState, EditInput, PaginationContainer, PaginationButton, PageInfo
} from './styles';

const ITEMS_PER_PAGE = 10;

const Reservas = () => {
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState('');
  const [statusFilter, setStatusFilter] = useState('Todos');
  const [startDate, setStartDate] = useState('');
  const [endDate, setEndDate] = useState('');
  const [isLoading, setIsLoading] = useState(true);
  const [reservas, setReservas] = useState([]);
  
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(0);
  
  const [editingId, setEditingId] = useState(null);
  const [currentDiscount, setCurrentDiscount] = useState('');

  const getReservaStatus = (reserva) => {
    const now = new Date();
    const inicio = new Date(reserva.dataInicio);
    const fim = new Date(reserva.dataFim);
    const twentyFourHoursFromNow = new Date(now.getTime() + 24 * 60 * 60 * 1000);

    if (fim < now) return 'Encerradas';
    if (now >= inicio && now <= fim) return 'EmAndamento';
    if (inicio > now && inicio <= twentyFourHoursFromNow) return 'FuturasProximas';
    if (inicio > twentyFourHoursFromNow) return 'FuturasNormais';
    return 'Todos';
  };
  
  const statusTextMap = {
    Encerradas: 'Encerrada',
    EmAndamento: 'Em Andamento',
    FuturasProximas: 'Próxima',
    FuturasNormais: 'Agendada'
  };

  useEffect(() => {
    const fetchReservas = async () => {
      setIsLoading(true);
      try {
        const params = new URLSearchParams();
        if (statusFilter !== 'Todos') params.append('status', statusFilter);
        if (startDate) params.append('startDate', new Date(startDate).toISOString());
        if (endDate) params.append('endDate', new Date(endDate).toISOString());
        params.append('page', currentPage);
        params.append('pageSize', ITEMS_PER_PAGE);
        
        const response = await api.get(`/reservas?${params.toString()}`);
        
        setReservas(response.data.items || []);
        setTotalPages(response.data.totalPages || 0);

      } catch (error) {
        console.error("Failed to fetch reservas: ", error);
        toast.error("Falha ao buscar as reservas.");
        setReservas([]); 
      } finally {
        setIsLoading(false);
      }
    };

    const handler = setTimeout(() => {
        fetchReservas();
    }, 500);

    return () => {
        clearTimeout(handler);
    };
  }, [statusFilter, startDate, endDate, currentPage]);

  const deleteReserva = async (id) => {
    if (window.confirm('Tem certeza que deseja excluir esta reserva?')) {
      try {
        await api.delete(`/reservas/${id}`);
        setReservas(reservas.filter(r => r.id !== id));
        toast.success("Reserva excluída com sucesso!");
      } catch {
        toast.error("Erro ao excluir a reserva.");
      }
    }
  };
  
  // --- Handlers for inline editing ---
  const handleEditClick = (reserva) => {
    setEditingId(reserva.id);
    setCurrentDiscount(reserva.desconto);
  };

  const handleCancelEdit = () => {
    setEditingId(null);
    setCurrentDiscount('');
  };

  const handleSaveDiscount = async (id) => {
    const newDiscountValue = parseFloat(currentDiscount);

    if (isNaN(newDiscountValue) || newDiscountValue < 0 || newDiscountValue > 30) {
        toast.error("O desconto deve ser um valor entre 0 e 30.");
        return;
    }

    try {
        const response = await api.patch(`/reservas/${id}/desconto`, newDiscountValue, {
            headers: { 'Content-Type': 'application/json' }
        });
        const updatedReserva = response.data; 

        setReservas(reservas.map(r => (r.id === id ? updatedReserva : r)));
        
        toast.success("Desconto atualizado com sucesso!");
        handleCancelEdit(); 
    } catch (error) {
        console.error("Failed to update discount:", error);
        toast.error("Erro ao atualizar o desconto.");
    }
  };


  // --- Client-side search for responsiveness ---
  const searchedReservas = useMemo(() => {
    // Ensure reservas is an array before filtering
    if (!Array.isArray(reservas)) return [];
    if (!searchTerm) return reservas;
    return reservas.filter(reserva =>
      reserva.titulo.toLowerCase().includes(searchTerm.toLowerCase()) ||
      reserva.responsavel.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }, [reservas, searchTerm]);

  // --- Formatting helpers ---
  const formatCurrency = (value) => (value || 0).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
  const formatDate = (dateString) => new Date(dateString).toLocaleString('pt-BR', { dateStyle: 'short', timeStyle: 'short' });

  const handlePageChange = (page) => {
    if (page >= 1 && page <= totalPages) {
        setCurrentPage(page);
    }
  };

  if (isLoading) {
    return (
      <PageContainer>
        <EmptyState>Carregando Reservas...</EmptyState>
      </PageContainer>
    );
  }

  return (
    <PageContainer>
      <PageHeader>
        <TitleContainer>
          <Calendar size={32} color="#4f46e5" />
          <Title>Minhas Reservas</Title>
        </TitleContainer>
        <PrimaryButton onClick={() => navigate("/Reservas/Create")}>
          <PlusCircle size={20} />
          Adicionar Nova Reserva
        </PrimaryButton>
      </PageHeader>

      <Card>
        <FilterContainer>
          <Input
            type="text"
            placeholder="Buscar por título ou responsável..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            style={{ flexGrow: 2 }}
          />
          <Input type="date" value={startDate} onChange={e => setStartDate(e.target.value)} />
          <Input type="date" value={endDate} onChange={e => setEndDate(e.target.value)} />
          <Select value={statusFilter} onChange={(e) => setStatusFilter(e.target.value)}>
            <option value="Todos">Todos os Status</option>
            <option value="EmAndamento">Em Andamento</option>
            <option value="FuturasProximas">Próximas 24h</option>
            <option value="FuturasNormais">Agendadas</option>
            <option value="Encerradas">Encerradas</option>
          </Select>
        </FilterContainer>

        <Table>
          <thead>
            <tr>
              <Th>Título</Th>
              <Th>Responsável</Th>
              <Th>Status</Th>
              <Th>Início</Th>
              <Th>Fim</Th>
              <Th>Desconto</Th>
              <Th>Valor Total</Th>
              <Th style={{ textAlign: 'right' }}>Ações</Th>
            </tr>
          </thead>
          <tbody>
            {searchedReservas.map((reserva) => {
              const status = getReservaStatus(reserva);
              const isEditing = editingId === reserva.id;
              return (
                <Tr key={reserva.id} status={status}>
                  <Td style={{ fontWeight: 500 }}>{reserva.titulo}</Td>
                  <Td>{reserva.responsavel}</Td>
                  <Td><StatusBadge status={status}>{statusTextMap[status]}</StatusBadge></Td>
                  <Td>{formatDate(reserva.dataInicio)}</Td>
                  <Td>{formatDate(reserva.dataFim)}</Td>
                  <Td>
                    {isEditing ? (
                      <EditInput
                        type="number"
                        value={currentDiscount}
                        onChange={(e) => setCurrentDiscount(e.target.value)}
                        autoFocus
                      />
                    ) : (
                      reserva.desconto > 0 ? formatCurrency(reserva.desconto) : '-'
                    )}
                  </Td>
                  <Td>{formatCurrency(reserva.valorTotal)}</Td>
                  <Td>
                    <div style={{ display: 'flex', justifyContent: 'flex-end', gap: '0.5rem' }}>
                      {isEditing ? (
                        <>
                          <ActionButton title="Salvar Desconto" onClick={() => handleSaveDiscount(reserva.id)}><Check size={18} /></ActionButton>
                          <ActionButton title="Cancelar Edição" onClick={handleCancelEdit}><X size={18} /></ActionButton>
                        </>
                      ) : (
                        <>
                          <ActionButton title="Detalhes da Reserva"><Info size={18} /></ActionButton>
                          <ActionButton title="Editar Reserva" onClick={() => handleEditClick(reserva)}><Edit size={18} /></ActionButton>
                          <ActionButton title="Excluir Reserva" onClick={() => deleteReserva(reserva.id)}><Trash2 size={18} /></ActionButton>
                        </>
                      )}
                    </div>
                  </Td>
                </Tr>
              );
            })}
          </tbody>
        </Table>
        {searchedReservas.length === 0 && (
          <EmptyState>Nenhuma reserva encontrada para os filtros selecionados.</EmptyState>
        )}
        {totalPages > 1 && (
            <PaginationContainer>
                <PaginationButton onClick={() => handlePageChange(currentPage - 1)} disabled={currentPage === 1}>
                    <ChevronLeft size={18} />
                    Anterior
                </PaginationButton>
                <PageInfo>Página {currentPage} de {totalPages}</PageInfo>
                <PaginationButton onClick={() => handlePageChange(currentPage + 1)} disabled={currentPage === totalPages}>
                    Próxima
                    <ChevronRight size={18} />
                </PaginationButton>
            </PaginationContainer>
        )}
      </Card>
    </PageContainer>
  );
};

export default Reservas;