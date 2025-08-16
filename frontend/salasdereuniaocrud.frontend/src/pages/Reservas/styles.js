import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
  min-height: 100vh;
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
  flex-wrap: wrap;
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
  min-width: 150px;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
  }
`;

export const Input = styled.input`${baseInputStyles}`;
export const Select = styled.select`${baseInputStyles}`;

export const EditInput = styled.input`
    ${baseInputStyles}
    padding: 0.25rem 0.5rem;
    max-width: 100px;
`;

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
  background-color: #f9fafb;
`;

export const Td = styled.td`
  padding: 1rem 1.5rem;
  font-size: 0.875rem;
  color: #374151;
  border-bottom: 1px solid #e5e7eb;
  vertical-align: middle;
`;

export const Tr = styled.tr`
  transition: background-color 0.2s ease-in-out;
  border-left: 4px solid transparent;

  ${({ status }) => {
    if (status === 'Encerradas') return 'border-left-color: #ef4444;'; // Red
    if (status === 'EmAndamento') return 'border-left-color: #f59e0b;'; // Amber
    if (status === 'FuturasProximas') return 'border-left-color: #10b981;'; // Green
    if (status === 'FuturasNormais') return 'border-left-color: #3b82f6;'; // Blue
    return '';
  }}

  &:last-child ${Td} {
    border-bottom: none;
  }
  &:hover {
    background-color: #f3f4f6;
  }
`;

export const ActionButton = styled.button`
  background: none;
  border: none;
  padding: 0.25rem;
  color: #6b7280;
  cursor: pointer;
  border-radius: 999px;
  transition: all 0.2s ease-in-out;
  
  &:hover {
    background-color: #e5e7eb;
    color: #1f2937;
  }
`;

const getStatusColor = (status) => {
    switch (status) {
        case 'Encerradas': return { bg: '#fee2e2', text: '#b91c1c' };
        case 'EmAndamento': return { bg: '#fef3c7', text: '#b45309' };
        case 'FuturasProximas': return { bg: '#d1fae5', text: '#047857' };
        case 'FuturasNormais': return { bg: '#dbeafe', text: '#1d4ed8' };
        default: return { bg: '#e5e7eb', text: '#4b5563' };
    }
};

export const StatusBadge = styled.span`
  display: inline-block;
  padding: 0.25rem 0.75rem;
  border-radius: 999px;
  font-size: 0.75rem;
  font-weight: 600;
  white-space: nowrap;
  background-color: ${({ status }) => getStatusColor(status).bg};
  color: ${({ status }) => getStatusColor(status).text};
`;

export const EmptyState = styled.div`
  padding: 3rem;
  text-align: center;
  color: #6b7280;
  font-size: 1rem;
`;

export const PaginationContainer = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 1rem 1.5rem;
    border-top: 1px solid #e5e7eb;
`;

export const PaginationButton = styled.button`
    display: flex;
    align-items: center;
    gap: 0.5rem;
    padding: 0.5rem 1rem;
    border: 1px solid #d1d5db;
    border-radius: 0.5rem;
    background-color: #ffffff;
    color: #374151;
    font-weight: 600;
    cursor: pointer;
    transition: all 0.2s ease;

    &:hover:not(:disabled) {
        background-color: #f9fafb;
    }

    &:disabled {
        color: #9ca3af;
        cursor: not-allowed;
    }
`;

export const PageInfo = styled.span`
    font-size: 0.875rem;
    font-weight: 500;
    color: #6b7280;
`;