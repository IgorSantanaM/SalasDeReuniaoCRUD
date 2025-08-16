import React, { useState } from 'react';
import { CalendarPlus, X } from 'lucide-react';
import {
  PageContainer, Container, TitleContainer, Title, Form, FormGroup, Label, Input,
  ErrorMessage, ButtonContainer, Button, CancelButton, SectionTitle, FormRow
} from './styles';
import api from '../../../services/api'; 
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

const ReservaCreation = () => {
  const navigate = useNavigate();
  const [titulo, setTitulo] = useState('');
  const [responsavel, setResponsavel] = useState('');
  const [dataInicio, setDataInicio] = useState('');
  const [dataFim, setDataFim] = useState('');
  const [participantesPrevistos, setParticipantesPrevistos] = useState('');
  const [valorHora, setValorHora] = useState('');
  const [desconto, setDesconto] = useState('');

  const [errors, setErrors] = useState({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  const validate = () => {
    const newErrors = {};
    const now = new Date();

    if (!titulo.trim()) newErrors.titulo = 'O título da reserva é obrigatório.';
    if (!responsavel.trim()) newErrors.responsavel = 'O nome do responsável é obrigatório.';
    
    if (!dataInicio) {
        newErrors.dataInicio = 'A data de início é obrigatória.';
    } else if (new Date(dataInicio) < now) {
        newErrors.dataInicio = 'A data de início não pode ser no passado.';
    }

    if (!dataFim) {
        newErrors.dataFim = 'A data de fim é obrigatória.';
    } else if (new Date(dataFim) <= new Date(dataInicio)) {
        newErrors.dataFim = 'A data de fim deve ser posterior à data de início.';
    }
    
    if (!participantesPrevistos || parseInt(participantesPrevistos, 10) <= 0) {
        newErrors.participantesPrevistos = 'O número de participantes deve ser maior que zero.';
    }

    if (valorHora === '' || parseFloat(valorHora) < 0) {
        newErrors.valorHora = 'O valor por hora não pode ser negativo.';
    }
    
    if (parseFloat(desconto) < 0 || parseFloat(desconto) > 30) {
        newErrors.desconto = 'O desconto deve ser um valor entre 0 e 30.';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    setIsSubmitting(true);
    
    const commandPayload = {
      titulo,
      responsavel,
      dataInicio: new Date(dataInicio).toISOString(),
      dataFim: new Date(dataFim).toISOString(),
      participantesPrevistos: parseInt(participantesPrevistos, 10),
      valorHora: parseFloat(valorHora),
      desconto: parseFloat(desconto) || 0
    };

    console.log("Enviando para a API:", commandPayload);

    try {
      const response = await api.post("reservas", commandPayload);
      console.log("Data sent successfully:", response.data);
      
      toast.success('Reserva cadastrada com sucesso!');
      await new Promise(resolve => setTimeout(resolve, 1000)); 

      navigate('/');
    } catch (error) {
      console.log(error);
      toast.error("Já existe uma reserva para este horário e dia.")
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <PageContainer>
      <Container>
        <TitleContainer>
          <CalendarPlus size={32} color="#4f46e5" />
          <Title>Cadastrar Nova Reserva</Title>
        </TitleContainer>

        <Form onSubmit={handleSubmit} noValidate>
          <div>
            <SectionTitle>Informações da Reserva</SectionTitle>
            <FormRow>
                <FormGroup>
                  <Label htmlFor="titulo">Título da Reserva</Label>
                  <Input type="text" id="titulo" value={titulo} onChange={(e) => setTitulo(e.target.value)} className={errors.titulo ? 'error' : ''} />
                  {errors.titulo && <ErrorMessage>{errors.titulo}</ErrorMessage>}
                </FormGroup>
                <FormGroup>
                  <Label htmlFor="responsavel">Responsável</Label>
                  <Input type="text" id="responsavel" value={responsavel} onChange={(e) => setResponsavel(e.target.value)} className={errors.responsavel ? 'error' : ''} />
                  {errors.responsavel && <ErrorMessage>{errors.responsavel}</ErrorMessage>}
                </FormGroup>
            </FormRow>
            <FormRow>
                 <FormGroup>
                  <Label htmlFor="dataInicio">Início</Label>
                  <Input type="datetime-local" id="dataInicio" value={dataInicio} onChange={(e) => setDataInicio(e.target.value)} className={errors.dataInicio ? 'error' : ''} />
                  {errors.dataInicio && <ErrorMessage>{errors.dataInicio}</ErrorMessage>}
                </FormGroup>
                <FormGroup>
                  <Label htmlFor="dataFim">Fim</Label>
                  <Input type="datetime-local" id="dataFim" value={dataFim} onChange={(e) => setDataFim(e.target.value)} className={errors.dataFim ? 'error' : ''} />
                  {errors.dataFim && <ErrorMessage>{errors.dataFim}</ErrorMessage>}
                </FormGroup>
            </FormRow>
          </div>

          <div>
            <SectionTitle>Valores e Participantes</SectionTitle>
             <FormRow>
                <FormGroup>
                  <Label htmlFor="participantes">Participantes Previstos</Label>
                  <Input type="number" id="participantes" value={participantesPrevistos} onChange={(e) => setParticipantesPrevistos(e.target.value)} className={errors.participantesPrevistos ? 'error' : ''} />
                  {errors.participantesPrevistos && <ErrorMessage>{errors.participantesPrevistos}</ErrorMessage>}
                </FormGroup>
                 <FormGroup>
                  <Label htmlFor="valorHora">Valor por Hora (R$)</Label>
                  <Input type="number" step="0.01" id="valorHora" value={valorHora} onChange={(e) => setValorHora(e.target.value)} className={errors.valorHora ? 'error' : ''} />
                  {errors.valorHora && <ErrorMessage>{errors.valorHora}</ErrorMessage>}
                </FormGroup>
                <FormGroup>
                  <Label htmlFor="desconto">Desconto (%)</Label>
                  <Input type="number" step="0.01" id="desconto" value={desconto} onChange={(e) => setDesconto(e.target.value)} className={errors.desconto ? 'error' : ''} />
                  {errors.desconto && <ErrorMessage>{errors.desconto}</ErrorMessage>}
                </FormGroup>
            </FormRow>
          </div>

          <ButtonContainer>
            <CancelButton type="button" onClick={() => navigate('/')}>
              <X size={20} style={{ marginRight: '0.5rem' }}  />
              Cancelar
            </CancelButton>
            <Button type="submit" disabled={isSubmitting}>
              {isSubmitting ? 'Salvando...' : 'Salvar Reserva'}
            </Button>
          </ButtonContainer>
        </Form>
      </Container>
    </PageContainer>
  );
};

export default ReservaCreation;