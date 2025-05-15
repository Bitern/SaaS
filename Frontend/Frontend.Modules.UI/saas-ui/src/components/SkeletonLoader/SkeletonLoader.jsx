import React, { useRef, useState, useLayoutEffect } from 'react';
import ContentLoader from 'react-content-loader';

export function SkeletonLoader({ isLoading, children, radius = 6 }) {
    const containerRef = useRef(null);
    const [childRects, setChildRects] = useState([]);
    const [containerDimensions, setContainerDimensions] = useState({ width: 0, height: 0 });
    const [isReadyToRenderSkeleton, setIsReadyToRenderSkeleton] = useState(false);

    useLayoutEffect(() => {
        if (!isLoading) {
            setChildRects([]);
            setContainerDimensions({ width: 0, height: 0 });
            setIsReadyToRenderSkeleton(false);
            return;
        }

        const updateMeasurements = () => {
            if (containerRef.current) {
                const containerRect = containerRef.current.getBoundingClientRect();
                setContainerDimensions({ width: containerRect.width, height: containerRect.height });

                const rects = [];
                const measurableElements = containerRef.current.querySelectorAll('*');

                measurableElements.forEach(element => {
                    if (element !== containerRef.current && !element.classList.contains('react-content-loader') && element.offsetParent !== null && element.offsetWidth > 0 && element.offsetHeight > 0) {
                        const rect = element.getBoundingClientRect();
                        rects.push({
                            x: rect.left - containerRect.left,
                            y: rect.top - containerRect.top,
                            width: rect.width,
                            height: rect.height,
                        });
                    }
                });

                if (rects.length > 0) {
                    setChildRects(rects);
                    setIsReadyToRenderSkeleton(true);
                } else {
                    setIsReadyToRenderSkeleton(false);
                }
            } else {
                setIsReadyToRenderSkeleton(false);
            }
        };

        let resizeObserver;
        if (containerRef.current) {
            resizeObserver = new ResizeObserver(updateMeasurements);
            resizeObserver.observe(containerRef.current);
        }

        const timeoutId = setTimeout(updateMeasurements, 100);

        return () => {
            clearTimeout(timeoutId);
            if (resizeObserver) resizeObserver.disconnect();
        };
    }, [isLoading, children]);

    return (
        <div ref={containerRef} style={{ position: 'relative' }}>
            {isLoading && isReadyToRenderSkeleton && containerDimensions.width > 0 && containerDimensions.height > 0 && (
                <div style={{ position: 'absolute', top: 0, left: 0, width: containerDimensions.width, height: containerDimensions.height, zIndex: 1 }}>
                    <ContentLoader
                        speed={2}
                        width={containerDimensions.width}
                        height={containerDimensions.height}
                        backgroundColor="#eaeced"
                        foregroundColor="#ffffff"
                    >
                        {childRects.map((rect, index) => (
                            <rect
                                key={index}
                                x={rect.x}
                                y={rect.y}
                                rx={radius}
                                ry={radius}
                                width={rect.width}
                                height={rect.height}
                            />
                        ))}
                    </ContentLoader>
                </div>
            )}
            <div style={{ visibility: isLoading ? 'hidden' : 'visible' }}>
                {children}
            </div>
        </div>
    );
}
