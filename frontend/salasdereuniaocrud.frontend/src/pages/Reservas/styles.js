import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
`;

export const PageHeader = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
`;

export const TitleContainer = styled.div`
  display: flex;
  align-items: center;
  gap: 0.75rem;
`;

export const Title = styled.h2`
  font-size: 1.75rem;
  font-weight: bold;
  color: #1f2937;
  margin: 0;
`;

export const PrimaryButton = styled.button`
  display: flex;
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
  transition: all 0.2s ease;

  &:hover {
    background-color: #4338ca;
  }
`;

export const Card = styled.div`
  background-color: white;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
  overflow: hidden;
`;

export const FilterContainer = styled.div`
  padding: 1.5rem;
  display: flex;
  gap: 1rem;
  background-color: #f9fafb;
  border-bottom: 1px solid #e5e7eb;
`;

const baseInputStyles = `
  padding: 0.75rem 1rem;
  border: 1px solid #d1d5db;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #ffffff;
  transition: all 0.2s ease-in-out;
  color: black;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
  }
`;

export const Input = styled.input`${baseInputStyles}`;
export const Select = styled.select`${baseInputStyles}`;

export const Table = styled.table`
  width: 100%;
  border-collapse: collapse;
`;

export const Th = styled.th`
  padding: 1rem 1.5rem;
  text-align: left;
  font-size: 0.75rem;
  font-weight: 600;
  color: #6b7280;
  text-transform: uppercase;
  letter-spacing: 0.05em;
  border-bottom: 1px solid #e5e7eb;
`;

export const Td = styled.td`
  padding: 1rem 1.5rem;
  font-size: 0.875rem;
  color: #374151;
  border-bottom: 1px solid #e5e7eb;
  vertical-align: middle;
`;

export const Tr = styled.tr`
  &:last-child ${Td} {
    border-bottom: none;
  }
  &:hover {
    background-color: #f9fafb;
  }
`;

export const ProductImage = styled.img`
  width: 48px;
  height: 48px;
  border-radius: 0.5rem;
  object-fit: cover;
`;

export const StockStatus = styled.span`
  font-weight: 600;
  color: ${({ stock }) => {
    if (stock === 0) return '#dc2626'; // Red
    if (stock <= 10) return '#f59e0b'; // Amber
    return '#10b981'; // Green
  }};
`;

export const ActionButton = styled.button`
  background: none;
  border: none;
  padding: 0.25rem;
  color: #6b7280;
  cursor: pointer;
  border-radius: 999px;
  
  &:hover {
    background-color: #e5e7eb;
    color: #1f2937;
  }
`;
