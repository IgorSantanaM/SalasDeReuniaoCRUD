import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const PageContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  height: 100%;
  padding: 1rem; // Padding base para telemóveis
  background-color: #f3f5f7;

  @media (min-width: 768px) {
    padding: 2rem; // Aumenta o padding para ecrãs maiores
  }
`;

export const ContentWrapper = styled.div`
  max-width: 500px;
`;

export const IconWrapper = styled.div`
  margin-bottom: 2rem;
  color: #4f46e5;
`;

export const Title = styled.h1`
  font-size: 2.5rem; // Tamanho de fonte menor para telemóveis
  font-weight: bold;
  color: #1f2937;
  margin: 0;

  @media (min-width: 768px) {
    font-size: 3rem; // Aumenta o tamanho da fonte para ecrãs maiores
  }
`;

export const Subtitle = styled.h2`
  font-size: 1.125rem; // Tamanho de fonte menor para telemóveis
  font-weight: 600;
  color: #4b5563;
  margin-top: 0.5rem;

  @media (min-width: 768px) {
    font-size: 1.25rem;
  }
`;

export const Message = styled.p`
  font-size: 1rem;
  color: #6b7280;
  margin-top: 1rem;
  margin-bottom: 2rem;
`;

export const HomeButton = styled(Link)`
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  padding: 0.75rem 1.5rem;
  background-color: #4f46e5;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  text-decoration: none;
  transition: all 0.2s ease;

  &:hover {
    background-color: #4338ca;
  }
`;
