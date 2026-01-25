
export const linkToPage = (mouseEvent: React.MouseEvent<HTMLAnchorElement>, state: object|null = null) => {
  mouseEvent.preventDefault();

  window.history.pushState(state, "random title :)", mouseEvent.currentTarget.href);

  const pushStateEvent = new CustomEvent('pushstate', {
    detail: {
      state: state,
    }
  });

  window.dispatchEvent(pushStateEvent);
}

export const goToPage = (pathname:string, state: object|null = null) => {
  window.history.pushState(state, "random title :)", pathname);

  const pushStateEvent = new CustomEvent('pushstate', {
    detail: {
      state: state,
    }
  });

  window.dispatchEvent(pushStateEvent);
}