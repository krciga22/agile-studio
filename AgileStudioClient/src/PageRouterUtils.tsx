
export const linkToPage = (mouseEvent: React.MouseEvent<Element, MouseEvent>, state: object|null = null) => {
  mouseEvent.preventDefault();

  const target = mouseEvent.currentTarget as HTMLAnchorElement;

  window.history.pushState(state, "random title :)", target.href);

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