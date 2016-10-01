const appRouter = (() => {
    let containerId = '#content';
    const router = new Navigo(null, true);

    router.on(() => {
        router.navigate('/home');
    });

    router.notFound(() => {
        router.navigate('/home');
    });

    router.on('/home', () => {
        navbarController.displayControls();
    });

    router.on('/login', () => {
        loginController.main(containerId)
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/logout', () => {
        loginController.logout()
            .then(() => {
                navbarController.displayControls();
            });
    });

    router.on('/todos', () => {
        todosController.main(containerId);
    });

    function start(container) {
        containerId = container;
        router.resolve();
    }

    return {
        start
    };
})();