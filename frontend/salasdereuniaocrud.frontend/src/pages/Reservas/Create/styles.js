import styled from 'styled-components';

export const PageContainer = styled.div`
  padding: 2rem;
  background-color: #f3f5f7;
  min-height: 100vh;
`;

export const Container = styled.div`
  max-width: 800px;
  margin: 0 auto;
  padding: 2rem;
  background-color: #ffffff;
  border-radius: 1rem;
  box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1), 0 2px 4px -2px rgb(0 0 0 / 0.1);
`;

export const TitleContainer = styled.div`
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 1.5rem;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid #e5e7eb;
`;

export const Title = styled.h2`
  font-size: 1.75rem;
  font-weight: bold;
  color: #1f2937;
  margin: 0;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 2rem;
`;

export const SectionTitle = styled.h3`
  font-size: 1.25rem;
  font-weight: 600;
  color: #374151;
  margin-bottom: 1.5rem;
`;

export const FormGroup = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;

export const FormRow = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 1.5rem;
  margin-bottom: 1.5rem;
`;


export const Label = styled.label`
  font-size: 0.875rem;
  font-weight: 500;
  color: #4b5563;
  margin-bottom: 0.5rem;
`;

export const Input = styled.input`
  padding: 0.75rem 1rem;
  border: 1px solid #d1d5db;
  color: black;
  border-radius: 0.5rem;
  font-size: 1rem;
  background-color: #f9fafb;
  transition: all 0.2s ease-in-out;
  width: 100%;

  &:focus {
    outline: none;
    border-color: #4f46e5;
    box-shadow: 0 0 0 3px rgba(79, 70, 229, 0.1);
  }

  &.error {
    border-color: #ef4444;
    &:focus {
      box-shadow: 0 0 0 3px rgba(239, 68, 68, 0.1);
    }
  }
`;

export const ErrorMessage = styled.span`
  color: #ef4444;
  font-size: 0.875rem;
  margin-top: 0.5rem;
`;

export const ButtonContainer = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 1rem;
  margin-top: 2rem;
  padding-top: 1.5rem;
  border-top: 1px solid #e5e7eb;
`;

export const Button = styled.button`
  display: flex;
  align-items: center;
  justify-content: center;
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

  &:disabled {
    background-color: #a5b4fc;
    cursor: not-allowed;
  }
`;

export const CancelButton = styled(Button)`
  background-color: #e5e7eb;
  color: #374151;

  &:hover {
    background-color: #d1d5db;
  }
`;