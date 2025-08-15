import React from 'react';
import { Frown, Home } from 'lucide-react';
import {
  PageContainer,
  ContentWrapper,
  IconWrapper,
  Title,
  Subtitle,
  Message,
  HomeButton,
} from './styles';

const NotFoundPage = () => {
  return (
    <PageContainer>
      <ContentWrapper>
        <IconWrapper>
          <Frown size={80} strokeWidth={1.5} />
        </IconWrapper>
        <Title>404</Title>
        <Subtitle>Página Não Encontrada</Subtitle>
        <Message>
          Oops! Parece que a página que você está a procurar não existe ou foi movida.
          Vamos levá-lo de volta para um lugar seguro.
        </Message>
        <HomeButton to="/">
          <Home size={20} style={{ marginRight: '0.5rem' }} />
          Voltar para o Painel
        </HomeButton>
      </ContentWrapper>
    </PageContainer>
  );
};

export default NotFoundPage;
