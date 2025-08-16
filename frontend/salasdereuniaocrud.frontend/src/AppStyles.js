import styled from "styled-components";

export const AppContainer = styled.div`
  display: flex;
  height: 100vh;
  width: 100vw;
  background-color: #f3f4f6;
  overflow: hidden; 
`;

export const PageWrapper = styled.div`
  flex-grow: 1;
  overflow-y: auto; 
  padding: 1.5rem;
  @media (max-width: 768px) {
    padding: 1rem;
  }
  `;