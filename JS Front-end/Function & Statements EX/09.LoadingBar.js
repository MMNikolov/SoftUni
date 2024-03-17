function solve(progress) {
    const renderProgressBar = (progress) => `[${'%'.repeat(progress / 10)}${'.'.repeat(10 - progress / 10)}]`;
    const renderProgress = (progress) => `${progress}% ${renderProgressBar(progress)}`;
    const isCompleted = progress === 100;

    console.log(renderProgress(progress));
    console.log(isCompleted ? 'Complete!' : 'Still loading...');
}

solve(30);
solve(100);
